using SharpSvn;
using SharpSvn.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<Model.MessageModel> result = new List<Model.MessageModel>();
            foreach (var item in Config.Repositories)
            {
                if (_client.GetLog(new Uri(item.Url), new SvnLogArgs(new SvnRevisionRange(new SvnRevision(startTime) , new SvnRevision(endTime))), out Collection<SvnLogEventArgs> logs))
                {

                    //后续操作，可以获取作者，版本号，提交时间，提交的message和提交文件列表等信息
                    foreach (var log in logs.Where(x=>x.Author==Config.UserName&& !string.IsNullOrEmpty(x.LogMessage)).OrderByDescending(x=>x.Time))
                    {
                            result.Add(new Model.MessageModel
                            {
                                UserName = log.Author,
                                Message = log.LogMessage,
                                SubmitTime = log.Time,
                                Repositories= item
                            });
                    }
                }
            }
            return result;


        }

    }
}
