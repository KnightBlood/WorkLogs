using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkLogs.Bll;
using WorkLogs.Common;
using WorkLogs.Model;

namespace WorkLogs.UI.MD
{
    /// <summary>
    /// ViewRegister.xaml 的交互逻辑
    /// </summary>
    public partial class ViewRegister : UserControl
    {
        public ViewRegister()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Title = "注册";
            Application.Current.MainWindow.Height = 300;
            Application.Current.MainWindow.Width = 300;

        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Content = new ViewLogin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //获取用户输入的信息
            string name = UserName.Text;
            string pwd = PassWord.Password;
            string Dname = DisplayName.Text;
            string OKpwd = OKPassWord.Password;
            //调用代码
            if (name == "" || pwd==""|| Dname==""|| OKpwd=="")
            {
                MessageBox.Show("各项不可为空！");
            }
            else
            {
                if (pwd != OKpwd)
                {
                    MessageBox.Show("密码不一致！");
                }
                else
                {
                    UsersBll usersBll = new UsersBll();
                    UsersModel user = usersBll.CheckByName(name, Dname, Md5Helper.EncryptString(pwd));
                    if (user != null)
                    {
                        MessageBox.Show("该用户已存在！");
                    }
                    else
                    {
                        MessageBox.Show("注册成功！");
                    }
                }
            }
        }



    }
}
