using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkLogs.Model;

namespace WorkLogs.Dal
{
    public partial class ProductsDal
    {
        public string GetPNmameByPnumber(string pNumber, int item)
        {
            //构造sql语句及参数
            string sql = "SELECT Product_C FROM Products WHERE ProductID = @ProductID";
            SqlParameter[] ps =
            {
                new SqlParameter("@ProductID", pNumber),
            };
            //执行并返回
            return (string)MSSqlHelper.ExecuteScalar(sql, ps);
        }
        public List<ProductsModel> GetList()
        {
            //构造查询sql语句
            string sql = "SELECT ProductID, Product_E, Product_C FROM Products";
            //执行查询
            DataTable dt = MSSqlHelper.GetDataTable(sql);
            //定义list，完成转存
            List<ProductsModel> list = new List<ProductsModel>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ProductsModel()
                {
                    ProductID = Convert.ToInt32(row["ProductID"]),
                    Product_E = row["Product_E"].ToString(),
                    Product_C = row["Product_C"].ToString(),
                });
            }
            return list;
        }

	    public int Insert(ProductsModel products)
        {
            //构造insert语句
            string sql = "INSERT INTO Products (Product_E, Product_C) VALUES (@Product_E,@Product_C)";
            //构造sql语句的参数
            SqlParameter[] ps = //使用数组初始化器
	        {
                new SqlParameter("@Product_E", products.Product_E),
                new SqlParameter("@Product_C", products.Product_C),
            };
            //执行插入操作
            return MSSqlHelper.ExecuteNonQuery(sql, ps);
        }

        public int Updata(ProductsModel products)
        {
            //构造sql语句及参数
            string sql = "UPDATE Products SET Product_E=@Product_E, Product_C=@Product_C WHERE ProductID=@ProductID";
            SqlParameter[] ps =
            {
                new SqlParameter("@ProductID", products.ProductID), 
                new SqlParameter("@Product_E", products.Product_E),
                new SqlParameter("@Product_C", products.Product_C),
            };
            //执行并返回
            return MSSqlHelper.ExecuteNonQuery(sql, ps);
        }
    }
}
