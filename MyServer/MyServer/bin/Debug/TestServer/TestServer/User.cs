using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestServer
{
    public class User
    {
        public Guid Uid { get; set; }
        public string Account { get; set; }
        public string Md5password { get; set; }
        public string Email { get; set; }

        public DateTime Time { get; set; }
    }
}