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
    public partial class OpenCloseOrder : UserControl
    {
        static OpenCloseOrder State;
        ObservableCollection<Product> products;
        Employee employee;

        public static OpenCloseOrder GetInstance(Employee empl)
        {
            if (State == null)
                State = new  OpenCloseOrder(empl);
            return State;
        }
        public OpenCloseOrder(Employee empl)
        {
            employee = empl;
            products = new ObservableCollection<Product>();
            IRepController controller = new RepositoryController();
            products = controller.GetDBProducts();
            InitializeComponent();
            ProductsList.Items.Clear();
            //ProductsList.DataContext = products;

            ProductsList.ItemsSource = products;
            ProductsList.DisplayMemberPath = "Name";
        }
    }
}
