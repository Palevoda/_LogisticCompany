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
    /// Логика взаимодействия для AdminEmployees.xaml
    /// </summary>
    public partial class AdminEmployees : UserControl
    {
        static AdminEmployees State;
        Employee employee;
        IRepController controller = new RepositoryController();
        public static AdminEmployees GetInstance(Employee empl)
        {
            if (State == null)
                State = new AdminEmployees(empl);
            return State;
        }
        public AdminEmployees(Employee empl)
        {
            employee = empl;            
            InitializeComponent();
            EmployeeWorkArea.Content = AdminEmployeeTable.GetInstance();
            
        }

        private void EmployeesViewer_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWorkArea.Content = AdminEmployeeTable.GetInstance();
        }

        private void EmployeesAdder_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWorkArea.Content = AdminAddEmployee.GetInstance();
        }

        private void EmployeeDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Id.Text == "")
                    throw new Exception("Укажите ID сотрудника, которого нужно кикнуть");

                int delete_id = Convert.ToInt32(Id.Text);
                Employee empl = controller.FindEmployeeById(delete_id);

                if (empl.role.Equals("Администратор") || empl.role.Equals("Admin"))
                    throw new Exception("Администратор не может удалять других администраторов");

                controller.DeleteEmployee(delete_id);
                EmployeeWorkArea.Content = AdminEmployeeTable.GetInstance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}
