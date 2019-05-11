using WorkLogs.Dal;
using WorkLogs.Model;


namespace WorkLogs.Bll
{
	/// <summary>
	/// Users
	/// </summary>
	public partial class UsersBll
	{
		private readonly UsersDal _dal=new UsersDal();

	    #region 我的方法

	    /// <summary>
	    /// 根据名字查询用户信息
	    /// </summary>
	    /// <param name="name"></param>
	    /// <returns></returns>
	    public UsersModel GetByName(string username)
	    {
	        return _dal.GetByName(username);
	    }

        public UsersModel CheckByName(string username,string displayName, string password)
        {
            return _dal.CheckByName(username, displayName, password);
        }
        public UsersModel CheckByDName(string displayName, string newpassword)
        {
            return _dal.CheckByDName(displayName, newpassword);
        }

        #endregion

        #region  BasicMethod

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

