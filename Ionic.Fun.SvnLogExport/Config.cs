using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ionic.Fun.SvnLogExport.Model;

namespace Ionic.Fun.SvnLogExport
{
    public class Config
    {
        /// <summary>
        /// Svn 用户名
        /// </summary>
        public static string UserName;

        /// <summary>
        /// Svn 密码
        /// </summary>
        public static string Password;

        /// <summary>
        /// Svn 仓储集合
        /// </summary>
        public static List<RepositoriesModel> Repositories;
      
    }
}
