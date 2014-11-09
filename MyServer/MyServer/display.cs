using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace MyServer
{
    class display:Page,IHttpHandler
    {
        public string ProcessRequest() 
        {
            List<User> list = DataOP.getUsers();
            
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>ASP数据库动态返回数据</title></head><body>");

            sb.Append("<table border='1'>");
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append("<tr>");
                User user = list[i];
                sb.Append("<td>" + user.Account + "</td><td>" + user.Email + "</td><td>" + user.Time + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("<hr/>");
            sb.Append("</body></html>");
           
            return sb.ToString();
        }
    }
}
