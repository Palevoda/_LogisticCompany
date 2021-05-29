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
    /// Логика взаимодействия для EmployeeTripsWindow.xaml
    /// </summary>
    public partial class EmployeeTripsWindow : UserControl
    {
        static EmployeeTripsWindow State;
        IRepController controller = new RepositoryController();
        Employee employee;

        public static EmployeeTripsWindow GetInstance(Employee empl)
        {
            if (State == null)
                State = new EmployeeTripsWindow(empl);

            if (State.employee.Id != empl.Id)
                State = new EmployeeTripsWindow(empl);

            return State;
        }

        public EmployeeTripsWindow(Employee empl)
        {
            employee = empl;
            InitializeComponent();
            TripsWorkingArea.Content = EmployeeTripsTable.GetInstance(employee);
        }

        private void GenerateTrip_Click(object sender, RoutedEventArgs e)
        {
            TripsWorkingArea.Content = EmployeeGenerateTrip.GetInstance(employee);
        }

        private void SendTrip_Click(object sender, RoutedEventArgs e)
        {
            if (NumberArea.Text != "")
            {                
                try
                {
                    int trip_id = Convert.ToInt32(NumberArea.Text);
                    Trip trip = controller.GetTrips().Where(t => t.Id == trip_id).FirstOrDefault();
                    if (trip.From.Id != employee.center.Id) throw new Exception("Отправить рейс может только сотрудник центра отправки или администратор");
                    if (trip.Status.Equals("Завершён")) throw new Exception("Нельзя отправить завершенный рейс");
                    if (trip.Status.Equals("В пути")) throw new Exception("Нельзя отправить отправленный рейс");
                    if (trip != null)
                    {
                        trip.SendTrip();
                        TripsWorkingArea.Content = EmployeeTripsTable.GetInstance(employee);
                    }
                    else throw new Exception("Проверьте правильность указанного Id");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Укажите Id рейса, над которым хотите совершить операцию");
        }

        private void ViewTrips_Click(object sender, RoutedEventArgs e)
        {
            TripsWorkingArea.Content = EmployeeTripsTable.GetInstance(employee);
        }

        private void CancelTrip_Click(object sender, RoutedEventArgs e)
        {
            if (NumberArea.Text != "")
            {
                try
                {
                    int trip_id = Convert.ToInt32(NumberArea.Text);
                    Trip trip = controller.GetTrips().Where(t => t.Id == trip_id).FirstOrDefault();
                    if (trip != null)
                    {
                        if (trip.Status.Equals("Завершён")) throw new Exception("Нельзя отменить завершенный рейс");
                        if (trip.Status.Equals("В пути")) throw new Exception("Нельзя отменить отправленный рейс");
                        else
                        {
                            trip.truck.CurrentCenter = trip.From;
                            controller.DelateTripsSlots(trip_id);
                            controller.DelateTrip(trip_id);
                            TripsWorkingArea.Content = EmployeeTripsTable.GetInstance(employee);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Укажите Id рейса, над которым хотите совершить операцию");
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
                    if (trip == null) throw new Exception("Не найден объект, с указанным Id. Проверьте правильность введенных данных");
                    if (trip.Status.Equals("Завершён")) throw new Exception("Нельзя завершить завершённый рейс");
                    if (trip.Status.Equals("Ожидает отправки")) throw new Exception("Нельзя завершить рейс, ожидающий отправки");
                    if (trip.To.Id != employee.center.Id) throw new Exception("Завершить рейс может только сотрудник центра назначения или администратор");
                    
                    ObservableCollection<TruckSlot> slots = controller.GetSlotsForTrip(trip);
                    //Прибывить к наименованию на складе кол-во  со слотов
                    Center centerTo = trip.To;
                    foreach (TruckSlot slot in slots)
                    {
                        ProductPosition position = controller.GetDBCenterProductsPosition(centerTo).Where(p => p.product.Id == slot.product.Id).FirstOrDefault();
                        if (position != null)
                            position.numberOfProduct += slot.total_umber;
                        else
                        {
                            ICommand command = new AddProductCommand(
                                employee,
                                Convert.ToString(slot.product.Name),
                                Convert.ToString(slot.product.Length),
                                Convert.ToString(slot.product.Height),
                                Convert.ToString(slot.product.Height),
                                Convert.ToString(slot.product.Weight),
                                Convert.ToString(slot.product.Cost),
                                slot.product.Unit_of_measurment,
                                Convert.ToString(slot.product.Mominal_number),
                                Convert.ToString(slot.total_umber)      
                              );
                            if (command.ifExecute()) command.Execute();
                        }
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
                        if (require != null)
                        {
                            if (require.Number == slot.total_umber)
                                controller.DelateRequier(require.Id);
                            else require.Number -= slot.total_umber;
                        }
                    }
                    //Удалить слоты
                    controller.DelateTripsSlots(trip_id);
                    //Завершить рейс
                    trip.CloseTrip();
                    trip.truck.closeTrip(centerTo);
                    TripsWorkingArea.Content = EmployeeTripsTable.GetInstance(employee);
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
