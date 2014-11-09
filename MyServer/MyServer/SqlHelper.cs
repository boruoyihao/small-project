using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    class SqlHelper
    {
        public static string connect = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;

        //测试用
        public static DataTable ExecuteTable(string sql)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = sql;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }
    }
}
