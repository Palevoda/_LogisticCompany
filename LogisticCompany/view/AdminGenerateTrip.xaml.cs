using LogisticCompany.model;
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
    public partial class AdminGenerateTrip : UserControl
    {
        static AdminGenerateTrip State;
        ObservableCollection<Center> centers = new ObservableCollection<Center>();
        ObservableCollection<Truck> trucks = new ObservableCollection<Truck>();
        IRepController controller = new RepositoryController();

        public static AdminGenerateTrip GetInstance()
        {
            if (State == null)
                State = new AdminGenerateTrip();
            return State;
        }
        public AdminGenerateTrip()
        {
            try
            {
                InitializeComponent();
                SetControlsContent();
                FromCenter.ItemsSource = centers;
                FromCenter.DisplayMemberPath = "CenterName";
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
                centers = controller.GetDBCenters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FromCenter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                trucks = controller.GetTrucks((Center)FromCenter.SelectedItem);
                Transport.ItemsSource = trucks;
                Transport.DisplayMemberPath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateTrip_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if ((Center)FromCenter.SelectedItem == null)
                    throw new Exception("Выберите пункт отправки");
                if ((Center)ToCenter.SelectedItem == null)
                    throw new Exception("Выберите пункт назначения");
                if ((Truck)Transport.SelectedItem == null)
                    throw new Exception("Выберите транспорт");

                Truck truck = (Truck)Transport.SelectedItem;
                Center center_from = (Center)FromCenter.SelectedItem;
                Center center_to = (Center)ToCenter.SelectedItem;

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
