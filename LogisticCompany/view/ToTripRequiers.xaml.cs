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
    /// <summary>
    /// Логика взаимодействия для ToTripRequiers.xaml
    /// </summary>
    public partial class ToTripRequiers : UserControl
    {
        static ToTripRequiers State;
        static IRepController controller = new RepositoryController();
        ObservableCollection<Require> Requiers;
        Employee employee;

        public static ToTripRequiers GetInstance(Employee employee)
        {
            if (State == null)
                State = new ToTripRequiers(employee);

            return State;
        }
        public ToTripRequiers(Employee employee)
        {
            this.employee = employee;
            Requiers = new ObservableCollection<Require>();
            InitializeComponent();
        }
    }
}
