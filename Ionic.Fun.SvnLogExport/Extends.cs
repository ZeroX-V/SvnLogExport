using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Fun.SvnLogExport
{
   public static class Extends
    {
        public static string LogToText(this List<Model.MessageModel> list) {
            var sb = new StringBuilder();
            var nowName = "";
            foreach (var item in list)
            {
                if (nowName!= item.Repositories.Name) {
                    if (nowName!="") {
                        sb.AppendLine("");
                        sb.AppendLine("");
                    }
                    sb.AppendLine(item.Repositories.Name);
                    sb.AppendLine("");
                    nowName = item.Repositories.Name;
                }
                sb.AppendLine(item.Message);
            }
            return sb.ToString();
        }
    }
}
