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
    public partial class AdminTransportTable : UserControl
    {
        static AdminTransportTable State;
        ObservableCollection<Truck> trucks;
        IRepController controller = new RepositoryController();
        public static AdminTransportTable GetInstance()
        {
         //   if (State == null)
                State = new AdminTransportTable();
          //  State.SetContext();
            return State;
        }
        public AdminTransportTable()
        {
            trucks = controller.GetTrucks();
            InitializeComponent();
            TransportTable.ItemsSource = trucks;
        }
        void SetContext()
        {
            trucks.Clear();
            foreach (Truck truck in controller.GetTrucks())
                trucks.Add(truck);
        }
    }
}
