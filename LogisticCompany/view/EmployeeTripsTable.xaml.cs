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
    /// <summary>
    /// Логика взаимодействия для EmployeeTripsTable.xaml
    /// </summary>
    public partial class EmployeeTripsTable : UserControl
    {
        static EmployeeTripsTable State;
        IRepController controller = new RepositoryController();
        ObservableCollection<TripDecorator> trips = new ObservableCollection<TripDecorator>();
        Employee employee;

        public static EmployeeTripsTable GetInstance(Employee empl)
        {
            State = new EmployeeTripsTable(empl);
            return State;
        }
        public EmployeeTripsTable(Employee empl)
        {
            employee = empl;
            InitializeComponent();
            TripTable.ItemsSource = trips;
            SetContext();
        }

        void SetContext()
        {
            if (trips.Count > 0) trips.Clear();
            ObservableCollection<Trip> trps = controller.GetTrips(employee.center);
            if (trps != null)
                foreach (Trip tr in trps)
                    trips.Add(new TripDecorator(tr));
            TripTable.ItemsSource = trips;
        }
    }
}
