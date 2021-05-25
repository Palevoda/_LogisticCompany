using LogisticCompany.model;
using LogisticCompany.ViewModel;
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

namespace LogisticCompany.view
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : UserControl
    {
        ICommand command;
        static AdminWindow State;
        Employee employee;
        public static AdminWindow GetInstance(Employee empl)
        {
            if (State == null)
                State = new AdminWindow(empl);
            return State;
        }
        public AdminWindow(Employee empl)
        {
            employee = empl;
            InitializeComponent();
            WindowUserName.Text = empl.Sorname;
            WindowUserRole.Text = empl.role;
        }

        private void ViewProducts_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = AdminProductsViewer.GetInstance(employee);
        }

        private void ViewEmployees_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = AdminEmployees.GetInstance(employee);
        }

        private void ViewRequiers_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = AdminRequiers.GetInstance();
        }

        private void ViewTrucks_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = AdminTransport.GetInstance();
        }

        private void ViewTrips_Click(object sender, RoutedEventArgs e)
        {
            WorkingArea.Content = AdminTripsWindow.GetInstance();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            command = new ExitCommand();
            if (command.ifExecute()) command.Execute();
        }
    }
}
