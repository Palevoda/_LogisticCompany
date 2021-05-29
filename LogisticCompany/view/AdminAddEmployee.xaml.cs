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
    /// Логика взаимодействия для AdminAddEmployee.xaml
    /// </summary>
    public partial class AdminAddEmployee : UserControl
    {
        static AdminAddEmployee State;
        ObservableCollection<Center> centers;
        IRepController controller = new RepositoryController();
        string[] roles = { "Администратор", "Сотрудник"};
        public static AdminAddEmployee GetInstance()
        {
            if (State == null)
                State = new AdminAddEmployee();
            return State;
        }

        public AdminAddEmployee()
        {
            try
            {
                centers = controller.GetDBCenters();
                InitializeComponent();
                AddEmployeeCenter.ItemsSource = centers;
                AddEmployeeCenter.DisplayMemberPath = "CenterName";
                AddEmployeeRole.ItemsSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void AddEmployeeInDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddEmployeeSorname.Text == "")
                    throw new Exception("Введите фамилию сотрудника");
                if (AddEmployeeName.Text == "")
                    throw new Exception("Введите имя сотрудника");
                if (AddEmployeeSecondName.Text == "")
                    throw new Exception("Введите отчество сотрудника");
                if (AddEmployeePhone.Text == "")
                    throw new Exception("Введите телефон сотрудника");
                if (AddEmployeeRole.SelectedItem == null)
                    throw new Exception("Выбеите роль сотрудника");
                if (AddEmployeePassword.Password == "")
                    throw new Exception("Введите пароль сотрудника");
                if ((Center)AddEmployeeCenter.SelectedItem == null)
                    throw new Exception("Укажите центр, за которым будет закреплен сотрудник");


                string sorname = Convert.ToString(AddEmployeeSorname.Text);
                string name = Convert.ToString(AddEmployeeName.Text);
                string second = Convert.ToString(AddEmployeeSecondName.Text);
                string phone = Convert.ToString(AddEmployeePhone.Text);
                string role = Convert.ToString(AddEmployeeRole.SelectedItem);
                string password = Convert.ToString(AddEmployeePassword.Password); 
                Center center = (Center)AddEmployeeCenter.SelectedItem;

                if (!Regex.IsMatch(phone, "[+]375[(][1-9]{2}[)][0-9]{3}-[0-9]{2}-[0-9]{2}", RegexOptions.None))
                    throw new Exception("Мобильный номер введен в неверной форме");

                if (password.Length < 6)
                    throw new Exception("Пароль не может быть менее 6 символов");

                Employee empl = new Employee(sorname, name, second, role, password, phone, center);
                controller.AddEmployeeInDB(empl);
                MessageBox.Show("Добавлен новый сотрудник");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
