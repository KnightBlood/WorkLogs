using System;
using System.Collections.Generic;
using System.Text;

namespace WorkLogs.Model
{
    public partial class ProductsModel
    {
        private int productID;
        private string product_E;
        private string product_C;
        public int ProductID { get => productID; set => productID = value; }
        public string Product_E { get => product_E; set => product_E = value; }
        public string Product_C { get => product_C; set => product_C = value; }
    }
}
