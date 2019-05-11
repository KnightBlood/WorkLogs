using System;
using System.Windows;
using System.Windows.Controls;
using WorkLogs.Bll;
using WorkLogs.Common;
using WorkLogs.Model;

namespace WorkLogs.UI.MD
{
    /// <summary>
    /// ViewUpdataPass.xaml 的交互逻辑
    /// </summary>
    public partial class ViewUpdataPass : UserControl
    {
        public ViewUpdataPass()
        {
            InitializeComponent();
        }
        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Title = "修改密码";
            Application.Current.MainWindow.Height = 300;
            Application.Current.MainWindow.Width = 300;
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new ViewDataGrid();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Dname = Application.Current.MainWindow.DataContext.ToString();
            string oldpwd = OldPassWord.Password;
            string newpwd = NewPassWord.Password;
            string newokpwd = NewPassWord.Password;
            //调用代码
            if (newpwd != newokpwd)
            {
                MessageBox.Show("新密码不一致！");
            }
            else
            {
                if (oldpwd == newpwd)
                {
                    MessageBox.Show("新旧密码不能相同！");
                }
                else
                {
                    UsersBll usersBll = new UsersBll();
                    UsersModel user = usersBll.CheckByDName(Dname, Md5Helper.EncryptString(newpwd));
                    if (user.PassWord.Equals(Md5Helper.EncryptString(oldpwd)))
                    {
                        user.OK = true ;
                        MessageBox.Show("修改密码成功！");
                    }
                    else
                    {
                        MessageBox.Show("原始密码错误！");
                    }
                }
            }
        }


    }
}
