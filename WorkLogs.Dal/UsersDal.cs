using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WorkLogs.Model;

namespace WorkLogs.Dal
{
   
    
    public partial class UsersDal
    {
        #region 
        public UsersModel CheckByName(string username,string displayName,string password)
        {
            //定义一个对象
            UsersModel user = null;
            //构造要查询的sql语句
            string sql = "SELECT UserName, DisplayName, PassWord FROM Users WHERE UserName=@UserName";
            SqlParameter p = new SqlParameter("@UserName", username);
            //使用helper进行查询,得到结果
            DataTable dt = MSSqlHelper.GetDataTable(sql, p);
            //判断是否查找到了
            if (dt.Rows.Count == 0)
            {
                //构造insert语句
                string INSERT = "INSERT INTO Users (UserName, DisplayName, PassWord) VALUES(@UserName,@DisplayName,@PassWord)";
                //构造sql语句的参数
                SqlParameter[] ps = //使用数组初始化器
                {
                    new SqlParameter("@UserName",username),
                    new SqlParameter("@DisplayName",displayName),
                    new SqlParameter("@PassWord",password),
                };
                //执行插入操作
                MSSqlHelper.ExecuteNonQuery(INSERT, ps);
            }
            return user;
        }
        public UsersModel CheckByDName(string displayName, string newpassword)
        {
            //定义一个对象
            UsersModel user = null;
            //构造要查询的sql语句
            string sql = "SELECT UserName, DisplayName, PassWord FROM Users WHERE DisplayName=@DisplayName";
            SqlParameter p = new SqlParameter("@DisplayName", displayName);
            //使用helper进行查询,得到结果
            DataTable dt = MSSqlHelper.GetDataTable(sql, p);
            //判断是否查找到了
            if (dt.Rows.Count > 0)
            {
                user = new UsersModel()
                {
                    PassWord = dt.Rows[0]["PassWord"].ToString(),
                };
                if (user.OK)
                {
                    //构造Update语句
                    string Update = "UPDATE Users SET PassWord=@PassWord WHERE DisplayName=@DisplayName";
                    //构造sql语句的参数
                    SqlParameter[] ps = //使用数组初始化器
                    {
                        new SqlParameter("@DisplayName",displayName),
                        new SqlParameter("@PassWord",newpassword)
                    };
                    //执行插入操作
                    MSSqlHelper.ExecuteNonQuery(Update, ps);
                }

            }
            return user;
        }


        public UsersModel GetByName(string username)
        {
            //定义一个对象
            UsersModel user = null;
            //构造要查询的sql语句
            string sql = "SELECT UserName, DisplayName, PassWord FROM Users WHERE UserName=@UserName";
            SqlParameter p = new SqlParameter("@UserName",username);
            //使用helper进行查询,得到结果
            DataTable dt = MSSqlHelper.GetDataTable(sql, p);
            //判断是否查找到了
            if (dt.Rows.Count > 0)
            {
                //用户名是存在的
                user = new UsersModel()
                {
                    UserName = username,
                    DisplayName = dt.Rows[0]["DisplayName"].ToString(),
                    PassWord = dt.Rows[0]["PassWord"].ToString(),
                };

            }
            return user;
        }
        #endregion
    }
}
