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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogisticCompany.view
{
    /// <summary>
    /// Логика взаимодействия для AdminTripsWindow.xaml
    /// </summary>
    public partial class AdminTripsWindow : UserControl
    {
        static AdminTripsWindow State;
        IRepController controller = new RepositoryController();
        public static AdminTripsWindow GetInstance()
        {
            if (State == null)
                State = new AdminTripsWindow();
            return State;
        }
        public AdminTripsWindow()
        {
            InitializeComponent();
            TripsWorkingArea.Content = AdminTripsTable.GetInstance();
        }

        private void GenerateTrip_Click(object sender, RoutedEventArgs e)
        {
            TripsWorkingArea.Content = AdminGenerateTrip.GetInstance();
            TripsWorkingArea.Content = AdminTripsTable.GetInstance();
        }

        private void ViewTrips_Click(object sender, RoutedEventArgs e)
        {
            TripsWorkingArea.Content = AdminTripsTable.GetInstance();
        }

        private void CloseTrip_Click(object sender, RoutedEventArgs e)
        {
            if (NumberArea.Text != "")
            {
                try
                {
                    int trip_id = Convert.ToInt32(NumberArea.Text);
                    //Получить слоты
                    Trip trip = controller.GetTrips().Where(t => t.Id == trip_id).FirstOrDefault();
                    ObservableCollection<TruckSlot> slots = controller.GetSlotsForTrip(trip);
                    //Прибывить к наименованию на складе кол-во  со слотов
                    Center centerTo = trip.To;
                    foreach (TruckSlot slot in slots)
                    {
                        ProductPosition position = controller.GetDBCenterProductsPosition(centerTo).Where(p => p.product.Id == slot.product.Id).FirstOrDefault();
                        position.numberOfProduct += slot.total_umber;
                    }
                    //Отнять с отбывшего склада
                    Center centerFrom = trip.From;
                    foreach (TruckSlot slot in slots)
                    {
                        ProductPosition position = controller.GetDBCenterProductsPosition(centerFrom).Where(p => p.product.Id == slot.product.Id).FirstOrDefault();
                        position.numberOfProduct -= slot.total_umber;
                    }
                    //Удалить заказы или отнять кол-во
                    foreach (TruckSlot slot in slots)
                    {
                        Require require = controller.GetDBRequiers().Where(r => r.FromCenter.Id == centerFrom.Id && r.ToCenter.Id == centerTo.Id && r.product.Id == slot.product.Id).FirstOrDefault();
                        if (require.Number == slot.total_umber)
                            controller.DelateRequier(require.Id);
                        else require.Number -= slot.total_umber;
                    }
                    //Удалить слоты
                    controller.DelateTripsSlots(trip_id);
                    //Завершить рейс
                    trip.CloseTrip();
                    trip.truck.closeTrip(centerTo);
                    TripsWorkingArea.Content = AdminTripsTable.GetInstance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Укажите Id рейса, над которым хотите совершить операцию");
        }

        private void CancelTrip_Click(object sender, RoutedEventArgs e)
        {
            if (NumberArea.Text != "")
            {
                try
                {
                    int trip_id = Convert.ToInt32(NumberArea.Text);
                    controller.DelateTripsSlots(trip_id);
                    controller.DelateTrip(trip_id);
                    TripsWorkingArea.Content = AdminTripsTable.GetInstance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Укажите Id рейса, над которым хотите совершить операцию");
        }

        private void SendTrip_Click(object sender, RoutedEventArgs e)
        {
            if (NumberArea.Text != "")
            {
                try
                {
                    int trip_id = Convert.ToInt32(NumberArea.Text);
                    Trip trip = controller.GetTrips().Where(t => t.Id == trip_id).FirstOrDefault();
                    //Сменить статус рейса
                    trip.SendTrip();
                    //Сменить статус фуры
                    trip.truck.SendInTrip();
                    controller.UpdateTrip(trip);
                    TripsWorkingArea.Content = AdminTripsTable.GetInstance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Укажите Id рейса, над которым хотите совершить операцию");
        }
    }
}
