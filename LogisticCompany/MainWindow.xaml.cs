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
using System.Collections.ObjectModel;

namespace LogisticCompany
{
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

                //Center center = new Center("MinskOne", "Минск", 95000f);
                //Center center1 = new Center("Vitba", "Витебск", 120000f);
                //Center center2 = new Center("GrodnoGodno", "Гродно", 110000f);
                //Center center3 = new Center("Beresta", "Брест", 110000f);
              //  Center center = repository.GetDBCenters().Where(p => p.CenterName.Equals("MinskOne")).FirstOrDefault();
              //  Employee employee = new Employee("Полевода2", "Александр", "Иванович", "Сотрудник", "1234", "+375297797593", center);
                
                Employee employee = repository.GetDBEmployee("Полевода", "+375297797593");
                IRepository IRep = new Repository();
               // IRep.AddEmployeeInDB(employee);

//                MessageBox.Show(employee.center.CenterName);
                //ObservableCollection<Center> centers = IRep.GetDBCenters();
                //ObservableCollection<Product> products = IRep.GetDBProducts();
                //Random rand = new Random();
                //foreach (Center cent in centers)
                //{
                //    if (cent.Id != 1)
                //        foreach (Product prod in products)
                //        {
                //            IRep.AddProductPositionInDB(new ProductPosition(prod, cent, rand.Next(400))); ;
                //        }
                //}

                //Truck truck = new Truck("AB1470-7", 40, 20, 20000, 90, false);
                //IRep.AddTruck(truck);



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
