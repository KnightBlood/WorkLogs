using System.Data;
using System.IO;
using System.Text;
using System;
using System.Data.SqlClient;
using System.Configuration;

namespace WorkLogs.Dal
{
    public abstract class MSSqlHelper
    {
        public static readonly string ConnStr= ConfigurationManager.ConnectionStrings["SqlServerConnectionString"].ConnectionString;
        //public static readonly string ConnStr = "Data Source=localhost;Initial Catalog = db_Works; Persist Security Info=True;User ID = Works; Password=Works";
        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps)
        {
            //创建连接对象
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                //创建命令对象
                SqlCommand cmd = new SqlCommand(sql, conn);
                //添加参数
                cmd.Parameters.AddRange(ps);
                //打开连接
                conn.Open();
                //执行命令，并返回受影响的行数
                return cmd.ExecuteNonQuery();
            }
        }


        public static object ExecuteScalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);

                conn.Open();
                //执行命令，获取查询结果中的首行首列的值，返回
                return cmd.ExecuteScalar();
            }
        }


        public static DataTable GetDataTable(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                //构造适配器对象
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                //构造数据表，用于接收查询结果
                DataTable dt = new DataTable();
                //添加参数
                adapter.SelectCommand.Parameters.AddRange(ps);
                //执行结果
                adapter.Fill(dt);
                //返回结果集
                return dt;
            }
        }

    }
}
