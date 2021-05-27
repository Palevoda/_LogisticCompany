using LogisticCompany.model;
using LogisticCompany.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace LogisticCompany.view
{
    public partial class ProductsObserver : UserControl
    {
        static ProductsObserver State;
        Employee employee;
        ICommand command;
        public static ProductsObserver GetInstance(Employee empl)
        {
            if (State == null)
                State = new ProductsObserver(empl);

            if (State.employee.Id != empl.Id)
                State = new ProductsObserver(empl);
            return State;
        }
        ProductsObserver(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
            ProductsObserverWorkSpace.Content = ProductsViewer.GetInstance(employee).Content;
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            ProductsObserverWorkSpace.Content = ProductsViewer.GetInstance(employee).Content;
        }

        private void AddProd_Click(object sender, RoutedEventArgs e)
        {
            ProductsObserverWorkSpace.Content = AddProduct.GetInstance(employee);
        }

        private void ChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductsObserverWorkSpace.Content = ReductProducts.GetInstance(employee);
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            command = new DeleteProductPosition(this);
            if (command.ifExecute()) command.Execute();
        }
    }
}
