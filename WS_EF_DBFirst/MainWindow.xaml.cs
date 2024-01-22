using DataLayerLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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

namespace WS_EF_DBFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NorthwindContext DB { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           DB = new NorthwindContext();
            try
            {
                     cboSuppliers.ItemsSource = (DB.Suppliers//.Select(x=>x.CompanyName)
                    .OrderBy(x =>x)
                    .ToList());
                //int numOfEm = DB.Employees.Count();
                //Debug.WriteLine($"{numOfEm} employees in DB");
            }
             catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void cboSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int? supplierid = ((Supplier) cboSuppliers.SelectedItem).SupplierId;
            if (supplierid != null)
            {
                lboProducts.ItemsSource = DB.Products.
                                            Where(x => x.Supplier!=null? x.Supplier.SupplierId == supplierid : false)
                                            //.Select(x => x.ProductName)
                                            .ToList();
            }
        }

        private void lboProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int? productid = ((Product)lboProducts.SelectedItem)?.ProductId;
            if(productid != null)
            {
                lboCustomer.ItemsSource = DB.OrderDetails.Where(x => x.ProductId == productid)
                    .Select(x=> x.Order.Customer.ContactName)
                    .Distinct()
                    .ToList();
            }
        }
    }
}
