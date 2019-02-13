﻿using Ionic.Fun.SvnLogExport;
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
            //manager.FindLocalRepositories(@"F:\ProjectCode\SVNProject");

            //保存仓储地址到文件
            // manager.SaveRepositoriesToFile(Config.Repositories);

            //从配置文件中读取仓储地址
            //manager.LoadRepositoriesToFile();

            var text = manager.GetCommitLog(Convert.ToDateTime("2018-02-11"), DateTime.Now).LogToText();
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
