using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkLogs.Common;
using WorkLogs.Bll;
using WorkLogs.Model;
using System.Windows.Controls.Primitives;

namespace WorkLogs.UI.MD
{
    /// <summary>
    /// ViewDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class ViewDataGrid : UserControl
    {
        public string dname = Application.Current.MainWindow.DataContext.ToString();
        private WorkLogsBll _workLogsBll = new WorkLogsBll();

        public ViewDataGrid()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Title = "个人工作日志";
            Application.Current.MainWindow.Height = 900;
            Application.Current.MainWindow.Width = 1600;

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new ViewLogin();
        }

        private string _oldColumnsFile = "works_columns.dll";

        private void SaveColumnOrder(DataGrid dgView)
        {
            using (StreamWriter sw = new StreamWriter(_oldColumnsFile, false))
            {
                foreach (DataGridColumn column in dgView.Columns)
                {
                    sw.WriteLine(column.DisplayIndex);
                }
            }
        }

        /// <summary>
        /// 读取列序
        /// </summary>
        /// <param name="dgView"></param>
        private void ReadColumnOrder(DataGrid dgView)
        {
            if (File.Exists(_oldColumnsFile))
            {
                //文件设置共享模式
                using (FileStream fs = new FileStream(_oldColumnsFile, FileMode.Open, FileAccess.Read,
                    FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        foreach (DataGridColumn column in dgView.Columns)
                        {
                            column.DisplayIndex = Convert.ToInt32(sr.ReadLine());
                        }
                    }
                }
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {

            LoadList();
            //加载列序
            ReadColumnOrder(dgvlist);

        }

        private void LoadList()
        {
            //定义键值对，存放查询条件

            dgvlist.AutoGenerateColumns = false;
            var list = new BindingCollection<WorkLogsModel>(_workLogsBll.GetList(dname));
            //list.AddingNew += this.List_AddingNew;
            //list.ListChanged += this.List_ListChanged;
            dgvlist.ItemsSource = list;

        }

        //private void List_AddingNew(object sender, AddingNewEventArgs e)
        //{

        //    //数据绑定
        //    var model = (WorkLogsModel) e.NewObject;
        //    model.DateTime = DateTime.Today;
        //    model.Name = dname;
        //    model.Whours = 8;
        //}

        //private void List_ListChanged(object sender, ListChangedEventArgs e)
        //{


        //}

        private void dgvList_ColumnDisplayIndexChanged(object sender, DataGridColumnEventArgs e)
        {
            //保存列序
            SaveColumnOrder((DataGrid) sender);
        }



        private void UpdatePass_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new ViewUpdataPass();
        }

        private void dgvlist_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //dgvlist.ItemsSource = new BindingCollection<WorkLogsModel>();
            WorkLogsModel logs = new WorkLogsModel()
            {
                ID= (e.Row.Item as WorkLogsModel).ID,
                DateTime = (e.Row.Item as WorkLogsModel).DateTime,
                Name = (e.Row.Item as WorkLogsModel).Name,
                ProductName = (e.Row.Item as WorkLogsModel).ProductName,
                Version = (e.Row.Item as WorkLogsModel).Version,
                Stage = (e.Row.Item as WorkLogsModel).Stage,
                Type = (e.Row.Item as WorkLogsModel).Type,
                Task = (e.Row.Item as WorkLogsModel).Task,
                Progress = (e.Row.Item as WorkLogsModel).Progress,
                Whours = Convert.ToInt32((e.Row.Item as WorkLogsModel).Whours),
                TProgress = (e.Row.Item as WorkLogsModel).TProgress,
                Workout = (e.Row.Item as WorkLogsModel).Workout,
                Problem = (e.Row.Item as WorkLogsModel).Problem,
            };
            if (Convert.ToString((e.Row.Item as WorkLogsModel).ID) == "0")
            {
                if (_workLogsBll.Add(logs))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("新增失败");
                }
            }
            else
            {

                if (_workLogsBll.Edit(logs))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }



        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new ViewProduct();
        }

        private void dgvlist_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Row.IsEditing)
            {
                if (Convert.ToString((e.Row.Item as WorkLogsModel).ID) == "0")
                {
                    (e.Row.Item as WorkLogsModel).DateTime = DateTime.Today;
                    (e.Row.Item as WorkLogsModel).Name = dname;
                    (e.Row.Item as WorkLogsModel).Whours = 8;

                }
            }
        }
    }
}