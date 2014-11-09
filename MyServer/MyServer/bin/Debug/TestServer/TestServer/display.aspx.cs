using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestServer
{
    public partial class display : System.Web.UI.Page
    {
        protected List<User> getList()
        {
            return DataOP.getUsers();
        }
        protected void aspList() 
        {
            List<User> list = DataOP.getUsers();
            for (int i = 0; i < list.Count; i++)
            {
                User user = list[i];
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                TableCell tc2 = new TableCell();
                TableCell tc3 = new TableCell();
                tc1.Text = user.Account;
                tc2.Text = user.Email;
                tc3.Text = user.Time.ToString();
                tr.Cells.Add(tc1);
                tr.Cells.Add(tc2);
                tr.Cells.Add(tc3);
                Table1.Rows.Add(tr);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            aspList();
            //Response.Write("nihao");
            //Console.WriteLine("The program is running");
            //string connect = WebConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
            //Response.Write(connect);
            List<User> list=DataOP.getUsers();
            Response.Write("第一种：后台response打印输出<br/>");
            for (int i = 0; i < list.Count; i++)
            {
                User user = list[i];
                Response.Write(user.Account + "     " + user.Email + "     " + user.Time +"<br/>");
            }

            Response.Write("<hr/>");


        }
    }
}