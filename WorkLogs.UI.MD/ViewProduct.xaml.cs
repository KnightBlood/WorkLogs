using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;

namespace WorkLogs.UI.MD
{
    /// <summary>
    /// ViewProduct.xaml 的交互逻辑
    /// </summary>

    public partial class ViewProduct : UserControl
    {
        private ProductsBll _productsBll = new ProductsBll();

        public ViewProduct()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            Application.Current.MainWindow.Title = "产品";
            Application.Current.MainWindow.Height = 635;
            Application.Current.MainWindow.Width = 400;
        }
        private string _oldColumnsFile = "products_columns.dll";

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
            //加载通讯录
            LoadList();
            //加载列序
            ReadColumnOrder(dgvlist);

        }

        private void LoadList()
        {
            //定义键值对，存放查询条件

            dgvlist.AutoGenerateColumns = false;
            var list = new BindingCollection<ProductsModel>(_productsBll.GetList());
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
            SaveColumnOrder((DataGrid)sender);
        }


        private void dgvlist_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //dgvlist.ItemsSource = new BindingCollection<WorkLogsModel>();
            ProductsModel products = new ProductsModel()
            {
                ProductID = (e.Row.Item as ProductsModel).ProductID,
                Product_C = (e.Row.Item as ProductsModel).Product_C,
                Product_E = (e.Row.Item as ProductsModel).Product_E
            };
            if (Convert.ToString((e.Row.Item as ProductsModel).ProductID) == "0")
            {
                if (_productsBll.Add(products))
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

                if (_productsBll.Edit(products))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content=new ViewDataGrid();
        }
    }
}
