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
using LogisticCompany.model;

namespace LogisticCompany.view
{
    public partial class EmployeeWindow : UserControl
    {
        static EmployeeWindow State;
        Employee employee;
        public Employee employe { get { return employee; } set { } }
        ICommand command;
        public static EmployeeWindow GetInstance(Employee employee)
        {
            if (State == null)
                State = new EmployeeWindow(employee);
            return State;
        }
        public static EmployeeWindow GetInstance()
        {
            if (State != null)
                return State;
            else 
            {
                MessageBox.Show("Пользователь не авторизован");
                return null;
            }
            
        }
        EmployeeWindow(Employee employee)
        {
            this.employee = employee;            
            InitializeComponent();
            WindowUserName.Text = employee.Sorname;
        }

        private void ShowAllProducts_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content =  ProductsObserver.GetInstance(employee);
        }

        private void ShowOrdersToCenter_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = OpenCloseOrder.GetInstance(employee);
        }

        private void ShowRequiersToTrip_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = ToTripRequiers.GetInstance(employee);
        }

        private void ShowTrips_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = Trips.GetInstance(employee);
        }

        private void Parking_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = TruckParkingViewer.GetInstance(employee);
        }
    }
}
