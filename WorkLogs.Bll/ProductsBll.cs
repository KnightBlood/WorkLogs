using System;
using System.Collections.Generic;
using WorkLogs.Dal;
using WorkLogs.Model;


namespace WorkLogs.Bll
{
	/// <summary>
	/// Project
	/// </summary>
	public partial class ProductsBll
    {
		private readonly ProductsDal _dal =new ProductsDal();

        #region 我的方法

        /// <summary>
        /// 根据生产编号查询客户名称
        /// </summary>
        /// <param name="pNumber">生产号</param>
        /// <param name="products"></param>
        /// <returns>项目名称</returns>

        public bool Add(ProductsModel products)
	    {
	        return _dal.Insert(products) > 0;
	    }
        public bool Edit(ProductsModel products)
        {
            return _dal.Updata(products) > 0;
        }

        public List<ProductsModel> GetList()
	    {
	        return _dal.GetList();
	    }

	    #endregion

        #region  BasicMethod

        #endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

