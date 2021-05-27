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
    /// Логика взаимодействия для EmployeeGenerateTrip.xaml
    /// </summary>
    public partial class EmployeeGenerateTrip : UserControl
    {
        static EmployeeGenerateTrip State;
        ObservableCollection<Center> centers = new ObservableCollection<Center>();
        ObservableCollection<Truck> trucks = new ObservableCollection<Truck>();
        IRepController controller = new RepositoryController();
        Employee employee;

        public static EmployeeGenerateTrip GetInstance(Employee empl)
        {
            State = new EmployeeGenerateTrip(empl);

            return State;
        }
        public EmployeeGenerateTrip(Employee empl)
        {
            try
            { 
            employee = empl;
            InitializeComponent();
            SetControlsContent();
            ToCenter.ItemsSource = centers;
            ToCenter.DisplayMemberPath = "CenterName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetControlsContent()
        {
            try
            {
                if (trucks != null) trucks.Clear();
                if (centers != null) centers.Clear();
                var cc = controller.GetDBCenters().Where(c => c.Id != 17);
                foreach (Center center in cc)
                    centers.Add(center);
                trucks = controller.GetTrucks(employee.center);
                Transport.ItemsSource = trucks;
                Transport.DisplayMemberPath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void GenerateTrip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((Center)ToCenter.SelectedItem == null)
                    throw new Exception("Выберите пункт назначения");
                if ((Truck)Transport.SelectedItem == null)
                    throw new Exception("Выберите транспорт");

                Truck truck = (Truck)Transport.SelectedItem;
                Center center_from = employee.center;
                Center center_to = (Center)ToCenter.SelectedItem;

                if (center_from.Id == center_to.Id)
                    throw new Exception("Центры не должны совпадать");

                //Получить все заказы с выбранного центра
                ObservableCollection<Require> requires = controller.GetDBRequiersFrom(center_from);
                //Получить слоты с каждого заказа
                ObservableCollection<TruckSlot> slots = new ObservableCollection<TruckSlot>();
                foreach (Require requier in requires)
                {
                    foreach (TruckSlot slot in TruckSlot.GetSlotsFromRequier(requier))
                        slots.Add(slot);
                }
                //Зарегистрировать рейс
                Trip trip = new Trip(slots, center_to, center_from, truck, "Ожидает отправки");
                controller.AddTripInDB(trip);
                //Занести слоты в БД
                ObservableCollection<Trip> trips = controller.GetTrips();
                trip = trips[trips.Count - 1];
                foreach (TruckSlot slot in slots)
                {
                    slot.SetTrip(trip);
                    controller.AddTruckSlotInDB(slot);
                }
                truck.SetBusy();
                MessageBox.Show("Сформирован новый рейс");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
