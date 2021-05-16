using LogisticCompany.model;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LogisticCompany.view
{
    public partial class TruckParkingViewer : UserControl
    {
        static TruckParkingViewer State;
        ObservableCollection<Truck> trucks;
        IRepController controller;
        Employee employee;
        public static TruckParkingViewer GetInstance(Employee empl)
        {
            if (State == null)
                State = new TruckParkingViewer(empl);
            return State;
        }
        public TruckParkingViewer(Employee empl)
        {
            controller = new RepositoryController();
            employee = empl;
            trucks = controller.GetTrucks(empl.center);          

            
            InitializeComponent();

            TrucksOnParking.ItemsSource = trucks;
        }
    }
}
