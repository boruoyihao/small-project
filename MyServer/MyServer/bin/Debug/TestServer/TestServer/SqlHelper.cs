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
    class SqlHelper
    {
        //从配置文件中读取数据库配置信息
        public static string connect = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;

        //测试用
        public static int ExecuteNonQuery(string sql)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = sql;
                    return command.ExecuteNonQuery();
                }
            }
        }
        //可以执行插入、删除、更新操作
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = sql;
                    foreach (SqlParameter parm in parameters)
                    {
                        command.Parameters.Add(parm);
                    }
                    //command.Parameters.AddRange(parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }
        //测试用
        public static object ExecuteScalar(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteScalar();
                }
            }
        }

        //执行查询操作
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parm in parameters)
                    {
                        cmd.Parameters.Add(parm);
                    }
                    //  cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteScalar();
                }
            }
        }

        public static DataSet ExecuteDataSet(string sql)
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
                    return dataset;
                }
            }
        }


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
        public static DataTable ExecuteTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

    }
}
