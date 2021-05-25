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
    public partial class AdminEmployeeTable : UserControl
    {
        static AdminEmployeeTable State;
        ObservableCollection<Employee> employees;
        IRepController controller = new RepositoryController();
        public static AdminEmployeeTable GetInstance()
        {
            if (State == null)
                State = new AdminEmployeeTable();

            State.SetContext();
            return State;
        }
        public AdminEmployeeTable()
        {
            employees = controller.GetDBEmployees();
            InitializeComponent();
            EmployeesTable.ItemsSource = employees;
        }

        void SetContext()
        {
            employees.Clear();
            foreach (Employee empl in controller.GetDBEmployees())
                employees.Add(empl);
        }
    }
}
