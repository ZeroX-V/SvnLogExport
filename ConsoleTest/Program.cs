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
            Config.Repositories = new List<Model.RepositoriesModel>() {
                new Model.RepositoriesModel{
                    Name="别名",
                    Url="svn 远程地址"
                }
            };
            LogManager manager = new LogManager();
            var list=  manager.GetCommitLog(Convert.ToDateTime("2018-12-01"),DateTime.Now);
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Repositories.Name}   {item.UserName}    {item.SubmitTime.ToString("yyyy-MM-dd")}   {item.Message} ");
            }
            Console.ReadKey();
        }
    }
}
