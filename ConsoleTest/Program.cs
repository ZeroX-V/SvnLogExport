using Ionic.Fun.SvnLogExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.UserName = "svn账号";
            Config.Password = "svn密码";
           
            LogManager manager = new LogManager();

           //查找本地svn项目
           // Config.Repositories =manager.FindLocalRepositories(@"F:\ProjectCode\SVNProject\SingleCode");

           //保存项目地址到文件
           // manager.SaveRepositoriesToFile(Config.Repositories);

            //从配置文件中读取
            Config.Repositories = manager.LoadRepositoriesToFile();
          
            var text = manager.GetCommitLog(Convert.ToDateTime("2018-02-11"), DateTime.Now).LogToText();
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
