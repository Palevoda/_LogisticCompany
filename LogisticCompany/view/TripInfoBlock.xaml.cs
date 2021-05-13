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
    public partial class TripInfoBlock : UserControl
    {
        static TripInfoBlock State;
        Employee employee;

        public static TripInfoBlock GetInstance(Employee employee)
        {
            if (State == null)
                State = new TripInfoBlock(employee);
            return State;
        }
        public TripInfoBlock(Employee empl)
        {
            employee = empl;
            InitializeComponent();
        }
    }
}
