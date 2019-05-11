using System.Data;
using System.IO;
using System.Text;
using System;
using Oracle.ManagedDataAccess.Client;

namespace WorkLogs.Dal
{
    public abstract class OracleHelper
    {
        public static string ConnStr;
        static OracleHelper()
        {
            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                ConnStr = sr.ReadLine();
            }
          
        }
        public static int ExecuteNonQuery(string sql, params OracleParameter[] ps)
        {
            //创建连接对象
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                //创建命令对象
                OracleCommand cmd = new OracleCommand(sql, conn);
                //添加参数
                cmd.Parameters.AddRange(ps);
                //打开连接
                conn.Open();
                //执行命令，并返回受影响的行数
                return cmd.ExecuteNonQuery();
            }
        }


        public static object ExecuteScalar(string sql, params OracleParameter[] ps)
        {
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.AddRange(ps);

                conn.Open();
                //执行命令，获取查询结果中的首行首列的值，返回
                return cmd.ExecuteScalar();
            }
        }


        public static DataTable GetDataTable(string sql, params OracleParameter[] ps)
        {
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                //构造适配器对象
                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
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
