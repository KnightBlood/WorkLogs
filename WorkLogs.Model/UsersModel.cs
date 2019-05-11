using System;
using System.Collections.Generic;
using System.Text;

namespace WorkLogs.Model
{
    public partial class UsersModel
    {
        private string userName;
        private string displayName;
        private string passWord;
        private string oripassWord;
        private bool ok;

        public string UserName
        {
            get => userName;
            set => userName = value;
        }

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string PassWord
        {
            get => passWord;
            set => passWord = value;
        }


        public string OriPassWord
        {
            get => oripassWord;
            set => oripassWord = value;
        }
        public bool OK
        {
            get => ok;
            set => ok = value;
        }
    }
}
