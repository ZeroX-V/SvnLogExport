using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Fun.SvnLogExport
{
    public class Model
    {
        public class MessageModel
        {
            public string UserName { get; set; }
            public string Message { get; set; }
            public DateTime SubmitTime { get; set; }
            public RepositoriesModel Repositories { get; set; }

        }


        public class RepositoriesModel
        {
            public string Name { get; set; }
            public string Url { get; set; }

        }

    }


}
