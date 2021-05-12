using LogisticCompany.model;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LogisticCompany.view
{
    /// <summary>
    /// Логика взаимодействия для ReductProducts.xaml
    /// </summary>
    public partial class ReductProducts : UserControl
    {
        static ReductProducts State;
        Employee employee;
        public static ObservableCollection<ProductPosition> ProductsOnStorage;
        IRepController controller;
        public static ReductProducts GetInstance(Employee employee)
        {
            if (State == null)
                State = new ReductProducts(employee);
            return State;
        }
        public static ReductProducts GetInstance()
        {
            return State;
        }
        public ReductProducts(Employee employee)
        {
            this.employee = employee;
            controller = new RepositoryController();
            ProductsOnStorage = controller.GetDBCenterProductsPosition(employee.center);
            InitializeComponent();
            ProductsViewerTable.ItemsSource = ProductsOnStorage;
        }
        public Employee GetEmployee()
        {
            return employee;
        }
    }
}
