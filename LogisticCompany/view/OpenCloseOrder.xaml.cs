using LogisticCompany.model;
using LogisticCompany.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LogisticCompany.view
{
    public partial class OpenCloseOrder : UserControl
    {
        static OpenCloseOrder State;
        ObservableCollection<Product> products;
        Employee employee;
        ICommand command;
        public ObservableCollection<Require> Requiers;

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
            Requiers = controller.GetDBRequiersTo(employee.center);
            InitializeComponent();

            ProductsList.Items.Clear();
            RequiersTable.ItemsSource = Requiers;
            ProductsList.ItemsSource = products;
            ProductsList.DisplayMemberPath = "Name";
        }

        private void AddRequier_Click(object sender, RoutedEventArgs e)
        {
            Product prod = (Product)ProductsList.SelectedValue;
            int number = Convert.ToInt32(NumberArea.Text);
            command = new CreateARequier(prod, employee.center, number);
            if (command.ifExecute()) command.Execute();
            command = new GetRequiers(employee); 
            if (command.ifExecute()) command.Execute();
        }

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            int Id = Convert.ToInt32(NumberArea.Text);
            command = new DelateRequier(Id);
            if (command.ifExecute()) command.Execute();
            Requiers.Remove(Requiers.Where(req=>req.Id == Id).FirstOrDefault());
        }
    }
}
