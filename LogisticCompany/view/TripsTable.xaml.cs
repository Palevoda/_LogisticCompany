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

namespace LogisticCompany.view
{
    public class TripDecorator
    {
        IRepController controller = new RepositoryController();
        public Trip trip { get; set; }
        public float cost { get; set; } = 0;
        public float weight { get; set; }  = 0;

        public int NumberOfSlots { get; set; } = 0;
        public TripDecorator(Trip trip)
        {
            this.trip = trip;
            this.trip.Slots = controller.GetSlotsForTrip(trip);
            NumberOfSlots = trip.Slots.Count;
            foreach(TruckSlot slot in trip.Slots)
            {
                cost += slot.total_cost;
                weight += slot.total_weight/1000;
            }
        }
    }
    public partial class TripsTable : UserControl
    {
        static TripsTable State;
        Employee employee;
        ObservableCollection<TripDecorator> trips;
        IRepController controller = new RepositoryController();
        public static TripsTable GetInstance(Employee empl)
        {
            if (State == null)
                State = new TripsTable(empl);
            if (State.employee.Id != empl.Id)
                State = new TripsTable(empl);
            //   State.AddTripsInCollection(empl);
            return State;
        }
        public TripsTable(Employee empl)
        {
            employee = empl;
            InitializeComponent();
            trips = new ObservableCollection<TripDecorator>();


            TripTable.ItemsSource = trips;
        }

        public void AddTripsInCollection(Employee employee)
        {
            trips.Clear();
            ObservableCollection<Trip> trps = new ObservableCollection<Trip>();
            var trip_trip = controller.GetTrips(employee.center).Where(tt=>tt.Status != "Завершён");
            foreach (Trip trip in trip_trip)
                trps.Add(trip);
            //trps = controller.GetTrips(employee.center);
            foreach (Trip tr in trps)
                trips.Add(new TripDecorator(tr));
        }

        public ObservableCollection<TripDecorator> GetTripsCollection()
        {
            return trips;
        }
    }
}
