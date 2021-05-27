using LogisticCompany.model;
using LogisticCompany.ViewModel;
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
    /// Логика взаимодействия для Trips.xaml
    /// </summary>
    public partial class Trips : UserControl
    {
        static Trips State;
        Employee employee;
        ObservableCollection<TripDecorator> trips;

        public static Trips GetInstance(Employee empl)
        {
            if (State == null)
                State = new Trips(empl);
            if (State.employee.Id != empl.Id)
                State = new Trips(empl);
            State.TripContentArea.Content = TripsTable.GetInstance(empl);
            return State;
        }
        public Trips(Employee empl)
        {
            employee = empl;
            IRepController controller = new RepositoryController();
            InitializeComponent();
            TripContentArea.Content = TripsTable.GetInstance(employee);
            trips = TripsTable.GetInstance(employee).GetTripsCollection();

        }
        private void TripsInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TripContentArea.Content = TripInfoBlock.GetInstance(employee, trips.Where(t => t.trip.Id == Convert.ToInt32(IdOfTripForAction.Text)).FirstOrDefault());
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void MakeBetterTrip_Click(object sender, RoutedEventArgs e)
        {
            IRepController controller = new RepositoryController();
            GenerateBestTrip trip = new GenerateBestTrip(controller.GetDBRequiersFrom(employee.center), controller.GetTrucks(employee.center).FirstOrDefault());
            Trip trip1 = new Trip(
                trip.GetTheBetterTrip().Slots,
                trip.GetTheBetterTrip().ToCenter,
                employee.center,
                trip.truck,
                "ожидает отправки");

          //
            controller.AddTripInDB(trip1);

            Trip trip_slot = controller.GetTripForSlots(employee.center);

            foreach (TruckSlot slot in trip1.Slots)
            {
                slot.SetTrip(trip_slot);
                slot.AddInDB();
            }
               
            // trip1.SaveSlots();
            
            TripContentArea.Content = TripsTable.GetInstance(employee);
        }
    }
}
