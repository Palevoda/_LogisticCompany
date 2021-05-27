using LogisticCompany.model;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AdminTransport.xaml
    /// </summary>
    public partial class AdminTransport : UserControl
    {
        static AdminTransport State;
        IRepController controller;
        public static AdminTransport GetInstance()
        {
            if (State == null)
                State = new AdminTransport();

            State.TransportWorkArea.Content = AdminTransportTable.GetInstance();
            return State;
        }
        public AdminTransport()
        {
            InitializeComponent();
            controller = new RepositoryController();
            TransportWorkArea.Content = AdminTransportTable.GetInstance();
        }

        private void TransportViewerr_Click(object sender, RoutedEventArgs e)
        {
            TransportWorkArea.Content = AdminTransportTable.GetInstance();
        }

        private void TransportAdder_Click(object sender, RoutedEventArgs e)
        {
            TransportWorkArea.Content = AdminAddTransport.GetInstance();
        }

        private void TransportDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Id.Text == "")
                    throw new Exception("Укажите ID транспорта, который хотите удалить");

                string delete_id = Convert.ToString(Id.Text);
                Truck truck = controller.GetTrucks().Where(t=>t.Id.Equals(delete_id)).FirstOrDefault();
                controller.RemoveTruck(truck);
                TransportWorkArea.Content = AdminTransportTable.GetInstance(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
