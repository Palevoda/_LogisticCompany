using LogisticCompany.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
    public partial class AdminTripsTable : UserControl
    {
        static AdminTripsTable State;
        IRepController controller = new RepositoryController();
        ObservableCollection<TripDecorator> trips = new ObservableCollection<TripDecorator>();

        public static AdminTripsTable GetInstance()
        {
            if (State == null)
                State = new AdminTripsTable();
            
            State.SetContext();
            return State;
        }
        public AdminTripsTable()
        {
            InitializeComponent();
            TripTable.ItemsSource = trips;
            SetContext();
        }

        void SetContext()
        {            
            if (trips.Count > 0) trips.Clear();
            ObservableCollection<Trip> trps = new ObservableCollection<Trip>();
            trps = controller.GetTrips();
            foreach (Trip tr in trps)
            trips.Add(new TripDecorator(tr));            
        }
    }
}
