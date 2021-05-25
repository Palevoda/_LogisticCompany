using LogisticCompany.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AdminAddTransport.xaml
    /// </summary>
    public partial class AdminAddTransport : UserControl
    {
        static AdminAddTransport State;
        ObservableCollection<Center> centers;
        IRepController controller = new RepositoryController();

        public static AdminAddTransport GetInstance()
        {
            if (State == null)
                State = new AdminAddTransport();
            return State;
        }

        public AdminAddTransport()
        {
            centers = controller.GetDBCenters();
            InitializeComponent();
            AddTruckCenter.ItemsSource = centers;
            AddTruckCenter.DisplayMemberPath = "CenterName";
        }

        private void AddTransportInDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddTruckRegistration.Text == "")
                    throw new Exception("Поле регистрационного знака не может быть пустым");
                if (AddTruckFuelConsmp.Text == "")
                    throw new Exception("Поле расхода топлива не может быть пустым");
                if (AddTruckSlots.Text == "")
                    throw new Exception("Поле кол-ва слотов не может быть пустым");
                if (AddTruckLoadCap.Text == "")
                    throw new Exception("Поле показателя грузоподъемности не может быть пустым");
                if (AddTruckVolumeCap.Text == "")
                    throw new Exception("Поле показателя объема трейлера не может быть пустым");
                if ((Center)AddTruckCenter.SelectedItem == null)
                    throw new Exception("Укажите центр");

                string registration = Convert.ToString(AddTruckRegistration.Text);
                float fuel_consmpt = (float)Convert.ToDouble(AddTruckFuelConsmp.Text);
                int truck_slots = Convert.ToInt32(AddTruckSlots.Text);
                int load_cap = Convert.ToInt32(AddTruckLoadCap.Text);
                int volume_cap = Convert.ToInt32(AddTruckVolumeCap.Text);
                Center center = (Center)AddTruckCenter.SelectedItem;


                if (!Regex.IsMatch(Convert.ToString(fuel_consmpt), "[0-9]+", RegexOptions.None))
                    throw new Exception("Показатели расхода топлива могут быть указаны только в цифрах");            
                if (!Regex.IsMatch(Convert.ToString(truck_slots), "[0-9]+", RegexOptions.None))
                    throw new Exception("Кол-во слотов может быть указано только в цифрах");
                if (!Regex.IsMatch(Convert.ToString(load_cap), "[0-9]+", RegexOptions.None))
                    throw new Exception("Показатели грузоподъемности могут быть указаны только в цифрах");
                if (!Regex.IsMatch(Convert.ToString(volume_cap), "[0-9]+", RegexOptions.None))
                    throw new Exception("Показатели объема трейлера могут быть указаны только в цифрах");
                if (!Regex.IsMatch(registration, "[A-Z]{2}[0-9]{4}-[1-7]{1}", RegexOptions.None))
                    throw new Exception("Введён некорректный регистрационный знак ТС");
                

                Truck truck = new Truck(registration, fuel_consmpt, truck_slots, load_cap, volume_cap, false, center);
                controller.AddTruck(truck);
                MessageBox.Show("Добавлена новая фура");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
