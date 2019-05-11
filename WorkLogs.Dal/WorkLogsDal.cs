using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess;
using Oracle.DataAccess;
using Oracle.SqlAndPlsqlParser;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkLogs.Model;

namespace WorkLogs.Dal
{
   public partial class WorkLogsDal
    {
        public int Insert(WorkLogsModel workLog)
        {
            //构造insert语句
            string sql = "INSERT INTO Works (DateTime, Name, ProductName, Version, Stage, Type, Task, Progress, Whours, TProgress, Workout, Problem) VALUES(@DateTime, @Name, @ProductName, @Version, @Stage, @Type, @Task, @Progress, @Whours, @TProgress, @Workout, @Problem)";
            //构造sql语句的参数
            SqlParameter[] ps = //使用数组初始化器
	        {
                new SqlParameter("@DateTime",workLog.DateTime),
                new SqlParameter("@Name",workLog.Name),
                new SqlParameter("@ProductName",workLog.ProductName),
                new SqlParameter("@Version",workLog.Version),
                new SqlParameter("@Stage",workLog.Stage),
                new SqlParameter("@Type",workLog.Type),
                new SqlParameter("@Task",workLog.Task),
                new SqlParameter("@Progress",workLog.Progress),
                new SqlParameter("@Whours",workLog.Whours),
                new SqlParameter("@TProgress",workLog.TProgress),
                new SqlParameter("@Workout",workLog.Workout),
                new SqlParameter("@Problem",workLog.Problem)
            };
            //执行插入操作
            return MSSqlHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="workLog"></param>
        /// <returns></returns>
        public int Updata(WorkLogsModel workLog)
        {
            //构造sql语句及参数
            string sql = "UPDATE Works SET DateTime= @DateTime, Name=@Name, ProductName=@ProductName, Version=@Version, Stage=@Stage, Type=@Type, Progress=@Progress, Whours=@Whours, TProgress=@TProgress, Workout=@Workout, Problem=@Problem WHERE ID=@ID";
            SqlParameter[] ps =
            {
                new SqlParameter("@ID",workLog.ID), 
                new SqlParameter("@DateTime",workLog.DateTime),
                new SqlParameter("@Name",workLog.Name),
                new SqlParameter("@ProductName",workLog.ProductName),
                new SqlParameter("@Version",workLog.Version),
                new SqlParameter("@Stage",workLog.Stage),
                new SqlParameter("@Type",workLog.Type),
                new SqlParameter("@Task",workLog.Task),
                new SqlParameter("@Progress",workLog.Progress),
                new SqlParameter("@Whours",workLog.Whours),
                new SqlParameter("@TProgress",workLog.TProgress),
                new SqlParameter("@Workout",workLog.Workout),
                new SqlParameter("@Problem",workLog.Problem)
            };
            //执行并返回
            return MSSqlHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(WorkLogsModel workLog)
        {
            //构造sql语句及参数
            string sql = "Delete From Works WHERE ID=@ID";
            SqlParameter[] ps =
            {
                new SqlParameter("@ID",workLog.ID)
            };
            //执行并返回
            return MSSqlHelper.ExecuteNonQuery(sql, ps);
        }


        /// <summary>
        /// 查询到列表
        /// </summary>
        /// <param name="?"></param>
        /// <param name="dname"></param>
        /// <returns></returns>
        public List<WorkLogsModel> GetList(string dname)
        {
            //构造查询sql语句
            string sql = "SELECT ID, DateTime, Name, ProductName, Version, Stage, Type, Task, Progress, Whours, TProgress, Workout, Problem  FROM Works where Name=@Name";
            //拼接查询条件
            List<SqlParameter> P = new List<SqlParameter>
            {
                new SqlParameter("@Name", dname)
            };
            //执行查询
            DataTable dt = MSSqlHelper.GetDataTable(sql, P.ToArray());
            //定义list，完成转存
            List<WorkLogsModel> list = new List<WorkLogsModel>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new WorkLogsModel()
                {
                    ID = Convert.ToInt32(row["ID"]),
                    DateTime = Convert.ToDateTime(row["DateTime"]),
                    Name = row["Name"].ToString(),
                    ProductName = row["ProductName"].ToString(),
                    Version = row["Version"].ToString(),
                    Stage = row["Stage"].ToString(),
                    Type = row["Type"].ToString(),
                    Task = row["Task"].ToString(),
                    Progress = row["Progress"].ToString(),
                    Whours = Convert.ToInt32(row["Whours"]),
                    TProgress = row["TProgress"].ToString(),
                    Workout = row["Workout"].ToString(),
                    Problem = row["Problem"].ToString()
                });
            }
            return list;
        }

        /// <summary>
        /// 查询指定天数内的列表
        /// </summary>
        /// <param name="days"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        //public List<Model.WorkLogsModel> GetListByDays(string days, Dictionary<string, string> dic)
        //{
        //    //构造查询sql语句
        //    string sql = "SELECT DateTime, Name, ProductID, Version, Stage, Type, Task, Progress, Whours, Problem  FROM WorkLogs WHERE DATE_SUB(CURDATE(), INTERVAL " + days + " DAY) <= date(CreateTime) ";
        //    //拼接查询条件
        //    List<SqlParameter> listP = new List<SqlParameter>();
        //    if (dic.Count > 0)
        //    {
        //        foreach (var pair in dic)
        //        {
        //            //sql+=" AND "+pair.Key+" LIKE '%"+pair.Value+"%'";
        //            //写成参数化，防注入
        //            sql += " AND " + pair.Key + " LIKE @" + pair.Key;
        //            listP.Add(new SqlParameter("@" + pair.Key, "%" + pair.Value + "%"));
        //        }
        //    }
        //    //执行查询
        //    DataTable dt = MSSqlHelper.GetDataTable(sql, listP.ToArray());
        //    //定义list，完成转存
        //    List<Model.WorkLogsModel> list = new List<Model.WorkLogsModel>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        list.Add(new Model.WorkLogsModel()
        //        {
        //            DateTime = Convert.ToDateTime(row["DateTime"]),
        //            Name = row["Name"].ToString(),
        //            ProductID = Convert.ToInt32(row["ProductID"]),
        //            Version = row["Version"].ToString(),
        //            Stage = row["Stage"].ToString(),
        //            Type = row["Type"].ToString(),
        //            Task = row["Task"].ToString(),
        //            Progress = row["Progress"].ToString(),
        //            Whours = Convert.ToInt32(row["Whours"]),
        //            Tprogress = row["Tprogress"].ToString(),
        //            Workout = row["Workout"].ToString(),
        //            Problem = row["Problem"].ToString()
        //        });
        //    }
        //    return list;
        //}
    }
}
