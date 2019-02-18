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
            Config.UserName = "zl";
            Config.Password = "123456";
           
            LogManager manager = new LogManager();

            //查找本地svn项目
            Config.Repositories= manager.FindLocalRepositories(@"F:\ProjectCode\SVNProject\SingleCode");

            //保存仓储地址到文件
            // manager.SaveRepositoriesToFile(Config.Repositories);

            //从配置文件中读取仓储地址
           // Config.Repositories=manager.LoadRepositoriesToFile();
            //Config.Repositories = new List<Model.RepositoriesModel>()
            //{
            //    new Model.RepositoriesModel{
            //        Name="xx",
            //        Url="https://192.168.0.14/svn/EjianFLowCqcppi/cqcppi"
            //    }
            //};

            var text = manager.GetCommitLog(Convert.ToDateTime("2019-02-01"), Convert.ToDateTime("2019-02-15")).LogToText();
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
