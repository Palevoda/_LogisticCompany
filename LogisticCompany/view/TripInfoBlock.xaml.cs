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
    public partial class TripInfoBlock : UserControl
    {
        IRepController controller = new RepositoryController();
        static TripInfoBlock State;
        TripDecorator trip;
        Employee employee;
        List<string> actions = new List<string>() { "ApproveArrival", "SendTrips" };
        public static TripInfoBlock GetInstance(Employee employee, TripDecorator trip)
        {
            if (State == null)
                State = new TripInfoBlock(employee, trip);
            return State;
        }
        public TripInfoBlock(Employee empl, TripDecorator trp)
        {
            employee = empl;
            trip = trp;
            InitializeComponent();
            TripActions.ItemsSource = actions;
            TripViewer.DataContext = trip;
            SlotsInfo.ItemsSource = trip.trip.Slots;
        }

        private void DoAction_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToString(TripActions.SelectedItem).Equals("ApproveArrival"))
                {
                    ObservableCollection<ProductPosition> productPositionsTo = controller.GetDBCenterProductsPosition(employee.center);
                    ObservableCollection<ProductPosition> productPositionsFrom = controller.GetDBCenterProductsPosition(trip.trip.From);

                    ProductPosition position, position2;
                    if (trip.trip.To.CenterName.Equals(employee.center.CenterName))
                    {
                        foreach (TruckSlot slot in trip.trip.Slots)
                        {
                            position = productPositionsTo.Where(pp => pp.product.Id == slot.product.Id).FirstOrDefault();
                            position2 = productPositionsFrom.Where(pp => pp.product.Id == slot.product.Id).FirstOrDefault();

                            if (position != null)
                            {
                                //Если найдена позиция товара на складе, соот. товару в слоте, просто добавляется кол-во товара
                                position.NumberOfProduct += slot.total_umber;
                                position2.NumberOfProduct -= slot.total_umber;
                                //Закрыть заказ или отнять требуемое количество 
                                Require req = controller.GetDBRequiersTo(employee.center).Where(r => r.product.Id == slot.product.Id).FirstOrDefault();
                                if (req != null)
                                    if (req.Number == slot.total_umber && req.product.Id == slot.product.Id)
                                        controller.DelateRequier(req.Id);
                                    else if (req.Number > slot.total_umber && req.product.Id == slot.product.Id)
                                        req.Number -= slot.total_umber;

                            }
                            else
                            {
                                //Если позиция не найдена, в базу данных добавляется новая, при просмотре товар буде скачан
                                controller.AddProductPositionInDB(new ProductPosition(slot.product, employee.center, slot.total_umber));
                            }
                        }
                        //Меняем ставим фуру на парковку, меняем статус
                        trip.trip.truck.CurrentCenter = employee.center;
                        trip.trip.truck.if_busy = false;
                        employee.center.trucks_on_parking.Add(trip.trip.truck);

                        trip.trip.Status = "Завершён";
                        MessageBox.Show("Рейс завершён");

                    }
                    else throw new Exception("Вы не можете принять рейс, который отправляете");
                }
                if (Convert.ToString(TripActions.SelectedItem).Equals("SendTrips"))
                {
                        try
                        {
                            int trip_id = this.trip.trip.Id;
                            Trip trip = controller.GetTrips().Where(t => t.Id == trip_id).FirstOrDefault();
                            if (trip != null)
                            {
                                //Сменить статус рейса
                                trip.SendTrip();
                                //Сменить статус фуры                    
                                //controller.UpdateTrip(trip);
                                trip.truck.SendInTrip();
                                //TripsWorkingArea.Content = AdminTripsTable.GetInstance();

                            }
                            else throw new Exception("Что-то пошло не так");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //ObservableCollection<ProductPosition> productPositions = controller.GetDBCenterProductsPosition(employee.center);
                    //ProductPosition position;
                    //if (trip.trip.From.CenterName.Equals(employee.center.CenterName))
                    //{
                    //    foreach (TruckSlot slot in trip.trip.Slots)
                    //    {
                    //        position = productPositions.Where(pp => pp.product.Id == slot.product.Id).FirstOrDefault();
                    //        if (position != null)
                    //        {
                    //            //Если найдена позиция товара на складе, соот. товару в слоте, просто добавляется кол-во товара
                    //           // position.NumberOfProduct -= slot.total_umber;
                    //        }
                    //        else
                    //        {
                    //            //Если позиция не найдена, в базу данных добавляется новая, при просмотре товар буде скачан
                    //            controller.AddProductPositionInDB(new ProductPosition(slot.product, employee.center, slot.total_umber));
                    //        }
                    //    }
                    //    //Меняем ставим фуру на парковку, меняем статус
                    //    trip.trip.truck.CurrentCenter = null;
                    //    trip.trip.truck.if_busy = true;
                    //    employee.center.trucks_on_parking.Remove(trip.trip.truck);
                    //    trip.trip.Status = "В пути";
                    //    MessageBox.Show("Рейс успешно отправлен");
                    //}
                    //else throw new Exception("Вы не можете отправить рейс, который напрявляется к вам");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
