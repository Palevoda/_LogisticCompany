using LogisticCompany.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LogisticCompany.view
{
    
    public partial class AdminProductsViewer : UserControl
    {
        static AdminProductsViewer State;
        ObservableCollection<ProductPosition> products;
        IRepController controller;
        Employee employee;
        public static AdminProductsViewer GetInstance(Employee empl)
        {
            if (State == null)
                State = new AdminProductsViewer(empl);
            return State;
        }
        public AdminProductsViewer(Employee empl)
        {
            controller = new RepositoryController();
            products = controller.GetAllProductsPosition();
            employee = empl;
            InitializeComponent();
            ProductsViewerTable.ItemsSource = products;



        }
    }
}
