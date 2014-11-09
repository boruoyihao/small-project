using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    class DataOP
    {
        public static List<User> getUsers()
        {
            List<User> list = new List<User>();
            DataTable table = SqlHelper.ExecuteTable("select * from t_user");
            DataRowCollection rows = table.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                User user = new User();
                DataRow row = rows[i];
                user.Account = (string)row["account"];
                user.Email = (string)row["email"];
                //user.Time = Convert.ToDateTime(row["time"]);
                user.Time = (string)row["time"];
                list.Add(user);
            }
            Console.WriteLine("程序正在执行");
            return list;
        }
    }
}
