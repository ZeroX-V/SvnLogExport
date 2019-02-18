using Newtonsoft.Json;
using SharpSvn;
using SharpSvn.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ionic.Fun.SvnLogExport.Model;

namespace Ionic.Fun.SvnLogExport
{
    public class LogManager
    {
        SvnClient _client;
        public LogManager()
        {
            if (_client == null)
            {
                SvnClient client = new SvnClient();
                client.Authentication.Clear();
                client.Authentication.UserNamePasswordHandlers += Authentication_UserNamePasswordHandlers;
                client.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;
                _client = client;
            }
        }


        private static void Authentication_UserNamePasswordHandlers(object sender, SvnUserNamePasswordEventArgs e)
        {
            //登录SVN的用户名和密码
            e.UserName = Config.UserName;
            e.Password = Config.Password;
        }

        //SSL证书有问题的，如果要忽略的话可以在这里忽略
        private static void Authentication_SslServerTrustHandlers(object sender, SvnSslServerTrustEventArgs e)
        {
            e.AcceptedFailures = e.Failures;
            e.Save = true;
        }

        /// <summary>
        /// 获取提交日志记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public List<Model.MessageModel> GetCommitLog(DateTime startTime, DateTime endTime)
        {
            endTime = endTime.AddDays(1);
            List<Model.MessageModel> result = new List<Model.MessageModel>();
           
            foreach (var item in Config.Repositories)
            {
                try
                {
                    Collection<SvnLogEventArgs> logs;
                    if (item.Url.StartsWith("https://"))
                    {
                        _client.GetLog(new Uri(item.Url), new SvnLogArgs(new SvnRevisionRange(startTime, endTime)), out logs);
                    }
                    else
                    {
                        _client.GetLog(item.Url, new SvnLogArgs(new SvnRevisionRange(startTime, endTime)), out logs);
                    }

                    //后续操作，可以获取作者，版本号，提交时间，提交的message和提交文件列表等信息
                    foreach (var log in logs.Where(x => x.Author == Config.UserName && !string.IsNullOrEmpty(x.LogMessage) && x.Time >= startTime && x.Time < endTime).OrderByDescending(x => x.Time))
                    {
                        result.Add(new Model.MessageModel
                        {
                            UserName = log.Author,
                            Message = log.LogMessage,
                            SubmitTime = log.Time,
                            Repositories = item
                        });
                    }
                }
                catch (Exception e)
                {

                }
                

            }
            return result;


        }

        /// <summary>
        /// 查找当前目录下的svn项目
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public List<RepositoriesModel> FindLocalRepositories(string root)
        {
            List<RepositoriesModel> result = new List<RepositoriesModel>();
            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(root);
            System.IO.DirectoryInfo[] ds = d.GetDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly);
            foreach (System.IO.DirectoryInfo var in ds)
            {
                if (Directory.Exists(var.FullName + @"\.svn"))
                {
                    result.Add(new RepositoriesModel
                    {
                        Name = var.Name,
                        Url = var.FullName
                    });
                }
            }
            return result;
        }


        /// <summary>
        /// 从配置文件中加载仓储文件
        /// </summary>
        /// <returns></returns>
        public List<RepositoriesModel> LoadRepositoriesToFile()
        {
            var saveDirectory = System.Threading.Thread.GetDomain().BaseDirectory + "config";
            string configPath = saveDirectory + @"\RepositoriesList.json";
            if (File.Exists(configPath))
            {
                var json = FileHelper.Read(configPath);
                return JsonConvert.DeserializeObject<List<RepositoriesModel>>(json);
            }
            return new List<RepositoriesModel>();

        }

        /// <summary>
        /// 保存仓储文件
        /// </summary>
        /// <param name="list"></param>
        public void SaveRepositoriesToFile(List<RepositoriesModel> list)
        {
            var saveDirectory = System.Threading.Thread.GetDomain().BaseDirectory + "config";
            if (!Directory.Exists(saveDirectory)) {
                Directory.CreateDirectory(saveDirectory);
            }
            string configPath = saveDirectory + @"\RepositoriesList.json";
            list.SaveToFile(configPath);
        }
    }
}
