using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    class ResponseModel
    {
        string strContentType;
        int contentLength = 0;
        byte[] arrContentBody;
        public int ContentLength
        {
            get { return contentLength; }
        }
        public ResponseModel(string strFileExtention, byte[] arrContentBody)
        {
            strContentType = GetContentTypeByFileExtention(strFileExtention);
            this.arrContentBody = arrContentBody;
            this.contentLength = arrContentBody.Length;
        }

        string GetContentTypeByFileExtention(string fileExtention)
        {
            switch (fileExtention)
            {
                case ".jpg":
                    return "image/jpeg";
                case ".aspx":
                case ".ashx":
                case ".jsp":
                case ".php":
                case ".html":
                    return "text/html";
                case ".css":
                    return "text/css";
                case ".js":
                    return "text/javascript";
                default: return "text/html";
            }
        }

        public byte[] GetResponseHeader()
        {
            /* HTTP/1.1 200 ok
            Content-Type: text/html;charset=utf-8
            Content-Length: 325 */
            string strHead = "HTTP/1.1 200 ok\r\n";
            strHead += "Content-Type:" + strContentType +
            ";charset=utf-8\r\n";
            strHead += "Content-Length:" + contentLength;
            //strHead += "Connection:Close";
         
            return Encoding.UTF8.GetBytes(strHead);
        }

        public byte[] GetContentBody()
        {
            return arrContentBody;
        }

        public string GetHeaderStr()
        {
            string strHead = "HTTP/1.1 200 ok\r\n";
            strHead += "Content-Type:" + strContentType + ";charset=utf-8\r\n";
            strHead += "Content-Length:" + contentLength + "\r\n";
            return strHead;
        }
    }
}
