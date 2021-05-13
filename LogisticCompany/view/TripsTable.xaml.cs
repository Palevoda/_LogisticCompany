using LogisticCompany.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
