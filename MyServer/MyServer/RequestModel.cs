using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    class RequestModel
    {
        private string path;

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }
        /* GET /index.html HTTP/1.1
 Accept: text/html, application/xhtml+xml, */
        public RequestModel(string strReqeust)
        {
            string[] arrStr = strReqeust.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            string[] arrStrFirstLine = arrStr[0].Split(' ');//GET /index.html HTTP/1.1

            path = arrStrFirstLine[1];// /index.html 
        }
    }
}
