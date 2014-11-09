using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    class DataOP
    {
        public static string addpassword = ConfigurationManager.AppSettings["addpassword"];//获取加盐字符串
        //向数据库中插入用户
        public static void insertUser(User user)
        {
            SqlHelper.ExecuteNonQuery("Insert into t_user(uid,account,md5password,email,time) values(@uid,@account,@md5password,@email,@time)",
                new SqlParameter("@uid", System.Guid.NewGuid().ToString()),
                new SqlParameter("@account", user.Account),
                new SqlParameter("@md5password", Md5.getMD5(user.Md5password + addpassword)),
                new SqlParameter("@email", user.Email),
                new SqlParameter("@time", DateTime.Now));

        }
        //用户登录时，查询数据库中是否存在用户
        public static Object isExistUser(string username, string md5password)
        {
            return SqlHelper.ExecuteScalar("select count(*) from t_user where account=@account and md5password=@md5password",
                new SqlParameter("@account", username),
                 new SqlParameter("@md5password", Md5.getMD5(md5password + addpassword)));
        }


        //用户登录时，查询用户名是否已经存在
        public static Object isExistUsername(string username)
        {
            return SqlHelper.ExecuteScalar("select count(*) from t_user where account=@account", new SqlParameter("@account", username));
        }

        //用户登录时，判断邮箱是否已经存在

        public static Object isExistEmail(string email)
        {
            return SqlHelper.ExecuteScalar("select count(*) from t_user where email=@email", new SqlParameter("@email", email));
        }

        //用户忘记密码时重置密码
        public static void forgetpassword(string email, string password)
        {
            SqlHelper.ExecuteNonQuery("Update t_user set md5password=@md5password where email=@email",
            new SqlParameter("@md5password", Md5.getMD5(password + addpassword)),
            new SqlParameter("@email", email));
        }

        //用户重置密码
        public static void resetpassword(string username, string password)
        {
            SqlHelper.ExecuteNonQuery("Update t_user set md5password=@md5password where account=@account",
            new SqlParameter("@md5password", Md5.getMD5(password + addpassword)),
            new SqlParameter("@account", username));
        }




        //获取用户登录次数
        public static int getcount(string username)
        {
            Object count = SqlHelper.ExecuteScalar(@"select count from account_count where account=@account",
                 new SqlParameter("@account", username));
            if (count == null)
                return 0;
            return (int)count;
        }
        //更新用户登录次数
        public static void updatecount(string username, int count, DateTime time)
        {
            SqlHelper.ExecuteNonQuery("Update account_count set count=@count,time=@time where account=@account",
            new SqlParameter("@account", username),
            new SqlParameter("@count", count),
            new SqlParameter("@time", time)
            );
        }

        //插入登录记录
        public static void insertRecord(string username, int count, DateTime time)
        {
            SqlHelper.ExecuteNonQuery("Insert into account_count(account,count,time) values(@account,@count,@time)",
             new SqlParameter("@account", username),
             new SqlParameter("@count", count),
             new SqlParameter("@time", time)
             );
        }
        //获取用户登录时间
        public static DateTime getLoginTime(string username)
        {
            DataTable table = SqlHelper.ExecuteTable("select time from account_count where account=@account",
                new SqlParameter("@account", username));
            DataRowCollection rows = table.Rows;
            DataRow row = rows[0];
            string str = (string)row["time"];
            return Convert.ToDateTime(str);
        }

        public static List<User> getUsers() 
        { 
            List<User> list=new List<User>();
            DataTable table = SqlHelper.ExecuteTable("select * from t_user");
            DataRowCollection rows = table.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                User user = new User();
                DataRow row = rows[i];
                user.Account = (string)row["account"];
                user.Email = (string)row["email"];
                user.Time = Convert.ToDateTime(row["time"]);
                list.Add(user);
            }
            Console.WriteLine("程序正在执行");
           return list;
        }
    }
}
