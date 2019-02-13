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
                    Name="项目1",
                    Url="https://192.168.0.11/svn/xxx/trunk"
                },
                  new Model.RepositoriesModel{
                    Name="项目1",
                    Url="https://192.168.0.11/svn/xxx/trunk"
                }

            };
            LogManager manager = new LogManager();
            var text = manager.GetCommitLog(Convert.ToDateTime("2019-02-11"), DateTime.Now).LogToText();
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
