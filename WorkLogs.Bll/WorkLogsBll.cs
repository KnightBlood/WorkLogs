using System.Collections.Generic;
using WorkLogs.Dal;
using WorkLogs.Model;

namespace WorkLogs.Bll
{
	/// <summary>
	/// WorkLogsBll
	/// </summary>
	public partial class WorkLogsBll
	{
		private readonly WorkLogsDal _dal=new WorkLogsDal();

	    #region 我的方法

	    public bool Add(WorkLogsModel workLog)
	    {
	        return _dal.Insert(workLog) > 0;
	    }

	    public bool Edit(WorkLogsModel workLog)
	    {
	        return _dal.Updata(workLog) > 0;
	    }
        public bool Del(WorkLogsModel workLog)
        {
            return _dal.Delete(workLog) > 0;
        }

        public List<WorkLogsModel> GetList(string dname)
	    {
	        return _dal.GetList(dname);
	    }


        //public List<WorkLogsModel> GetListByDays(string days, string strname)
        //{
        //    return _dal.GetListByDays(days , strname);
        //}

	    #endregion

        #region  BasicMethod

        #endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

