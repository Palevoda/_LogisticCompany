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
    /// Логика взаимодействия для TripsTable.xaml
    /// </summary>
    public partial class TripsTable : UserControl
    {
        static TripsTable State;
        Employee employee;

        public static TripsTable GetInstance(Employee empl)
        {
            if (State == null)
                State = new TripsTable(empl);
            return State;
        }
        public TripsTable(Employee empl)
        {
            employee = empl;
            InitializeComponent();
        }
    }
}
