using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    class User
    {
        public Guid Uid { get; set; }
        public string Account { get; set; }
        public string Md5password { get; set; }
        public string Email { get; set; }

       // public DateTime Time { get; set; }
        public string Time { get; set; }
    }
}
