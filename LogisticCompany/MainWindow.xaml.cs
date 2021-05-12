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
using System.Security.Cryptography;
namespace LogisticCompany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MainWindow MAINWINDOW;

        public static MainWindow GetInstance()
        {
            if (MAINWINDOW == null)
                MAINWINDOW = new MainWindow();

            return MAINWINDOW;
        }
        public MainWindow()
        {
            try
            {
                MAINWINDOW = this;
                InitializeComponent();
                Repository repository = new Repository();
                DataBase context = repository.GetContext();

                Center center = new Center("MinskOne", "Минск", 100000f);
                Employee employee = new Employee("Полевода", "Александр", "Иванович", "Сотрудник", "1234", "+375297797593", center);
                IRepository IRep = new Repository();

               // string md5 = Convert.ToString(MD5.Create());
                
               // var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
              // IRep.AddCenterInDB(center);
              // IRep.AddEmployeeInDB(employee);

                //Установка контентом авторизационное окно
              // WINDOW.Content = new LogisticCompany.view.Authorization().Content;
                //Установка контентом окна просмотров
                LogisticCompany.MainWindow.GetInstance().Content = LogisticCompany.view.EmployeeWindow.GetInstance(employee).Content;
                    
                //   WorkingArea.Content = 
                //     MessageBox.Show(Convert.ToString(context.Trucks.Count()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Truck truck = new Truck("AA1749-7", 9.7f, 20, 20000, 20000, false );
            //Truck truck1 = context.Trucks.Find(truck.Id);
            //context.Trucks.Remove(truck1);
            //context.SaveChanges();
            //Center center = new Center("Center1", "Minsk", 75000);
            //center.products.Add(new Product(5f,5f,5f,5f,5f, 6, "кг"));
            //center.products.Add(new Product(6f, 6f, 6f, 6f, 6f, 6, "кг"));
            //context.Centers.Add(center);

            // context.SaveChanges();

            // MessageBox.Show(Convert.ToString(context.Centers.Find(1).products.Count()));
            // MessageBox.Show(Convert.ToString(context.Trucks.Count()));

        }
    }
}
