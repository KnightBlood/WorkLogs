using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkLogs.Bll;
using WorkLogs.Common;
using WorkLogs.Model;

namespace WorkLogs.UI.MD
{
    /// <summary>
    /// ViewLogin.xaml 的交互逻辑
    /// </summary>
    public partial class ViewLogin : UserControl
    {
        public ViewLogin()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Title = "登录";
            Application.Current.MainWindow.Height = 300;
            Application.Current.MainWindow.Width = 300;

        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ErrorPoint_Loaded(object sender, RoutedEventArgs e)
        {
            var ui = sender as StackPanel;
            var errorheight = ui.Margin.Top - error.Height - 10;
            //error.Margin = new Thickness(ui.Margin.Left, errorheight, this.ActualWidth - ui.Margin.Left, this.ActualHeight - errorheight);
        }
        private void Login_Click(object sender, RoutedEventArgs e) 
        {
            //获取用户输入的信息
            string name = UserName.Text;
            string pwd = PassWord.Password;
            //调用代码
            if (UserName.SelectedText == null)
            {
                MessageBox.Show("用户名或密码为空");
            }
            else
            {
                UsersBll usersBll = new UsersBll();
                UsersModel user = usersBll.GetByName(name);
                if (user == null)
                {
                    MessageBox.Show("用户名错误");
                }
                else
                {
                    
                    if (user.PassWord.Equals(Md5Helper.EncryptString(pwd)))
                    {
                        //将登陆者传递给主窗口
                        Application.Current.MainWindow.DataContext = user.DisplayName;
                        Application.Current.MainWindow.Content = new ViewDataGrid();
                        

                    }
                    else
                    {
                        MessageBox.Show("密码错误");
                    }
                }
            }
        }

        private void 注册_MouseUp(object sender, MouseButtonEventArgs e)
        {
            App.Current.MainWindow.Content =new ViewRegister();
        }


    }
}
