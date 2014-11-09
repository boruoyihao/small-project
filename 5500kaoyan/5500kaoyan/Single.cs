using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5500kaoyan
{
    class Single
    {
        Single() { }
        public static IsolatedStorageSettings Instance {
            get {
                return Nested._appSettings;
            }
        }
        class Nested 
        {
            static Nested() { }
            internal static readonly IsolatedStorageSettings _appSettings = IsolatedStorageSettings.ApplicationSettings;
        }
    }
}
