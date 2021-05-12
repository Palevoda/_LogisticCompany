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
    /// Логика взаимодействия для Trips.xaml
    /// </summary>
    public partial class Trips : UserControl
    {
        static Trips State;
        Employee employee;

        public static Trips GetInstance(Employee empl)
        {
            if (State == null)
                State = new Trips(empl);
            return State;
        }
        public Trips(Employee empl)
        {
            employee = empl;
            InitializeComponent();
            TripContentArea.Content = TripsTable.GetInstance(employee);

        }
    }
}
