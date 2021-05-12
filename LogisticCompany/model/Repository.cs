using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LogisticCompany.model
{
    public class Repository : IRepository
    {
        static Repository state;
        DataBase db_context;

        public static Repository GetInstance()
        {
            if (state == null)
                state = new Repository();

            return state;
        }
        public Repository()
        {
            db_context = new DataBase();
        }

        public DataBase GetContext()
        {
            if (db_context == null)
                db_context = new DataBase();
            return db_context;
        }
        //Получить коллекции
        public DbSet GetDBCenters()
        {
            return db_context.Centers;
        }
        public void AddCenterInDB(Center center)
        {
            db_context.Centers.Add(center);
            db_context.SaveChanges();
        }
        //Сотрудники
        public DbSet GetDBEmployees()
        {
            return db_context.Employees;
        }
        public Employee GetDBEmployee(string sorname, string phone)//+
        {
            Employee employee = db_context.Employees.Where(e => e.Sorname.Equals(sorname) && e.phoneNumber.Equals(phone)).Include(ee => ee.center).FirstOrDefault();
            if (employee != null) return employee;
            else return null;
        }
        public void AddEmployeeInDB(Employee employee)
        {
            db_context.Employees.Add(employee);
            db_context.SaveChanges();
        }
        //Продукты
        public ObservableCollection<Product> GetDBProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
              foreach (Product product in db_context.Products)
                    products.Add(product); 
            return products;
        }
        public void AddProductInDB(Product product)
        {
            db_context.Products.Add(product);
            db_context.SaveChanges();
        }
        public void UpdateProductInDataBase(Product product)
        {
            Product prod = db_context.Products.Find(product.Id);
            prod.ChangeProperties(product);
            db_context.SaveChanges();
        }
        //Заказы
        public ObservableCollection<Require> GetDBRequiers()
        {
            ObservableCollection<Require> requiers = new ObservableCollection<Require>();
            foreach (Require requier in db_context.Requires.Include(r => r.product).Include(rr => rr.FromCenter).Include(rrr => rrr.ToCenter))
                requiers.Add(requier);
            return requiers;
        }
        public ObservableCollection<Require> GetDBRequiersFrom(Center center)
        {
            ObservableCollection<Require> requiers = new ObservableCollection<Require>();
            foreach (Require requier in db_context.Requires.Include(r => r.product).Include(rr => rr.FromCenter).Include(rrr => rrr.ToCenter).Where(req => req.FromCenter.CenterName.Equals(center.CenterName)))
                requiers.Add(requier);
            return requiers;
        }
        public ObservableCollection<Require> GetDBRequiersTo(Center center)
        {
            ObservableCollection<Require> requiers = new ObservableCollection<Require>();
            foreach (Require requier in db_context.Requires.Include(r => r.product).Include(rr => rr.FromCenter).Include(rrr => rrr.ToCenter).Where(req => req.ToCenter.CenterName.Equals(center.CenterName)))
                requiers.Add(requier);
            return requiers;
        }
        public void AddRequierInDB(Require require)
        {
            db_context.Requires.Add(require);
            db_context.SaveChanges();
        }

        //Рейсы
        public DbSet GetDBTrips()
        {
            return db_context.Trips;
        }
        public DbSet GetDBTrucks()
        {
            return db_context.Trucks;
        }
        public ObservableCollection<Truck> GetDBCenterParking(Center center)
        {
            ObservableCollection<Truck> trucks = new ObservableCollection<Truck>();
            foreach (Truck prod in db_context.Trucks.Include(t=>t.CurrentCenter).Where(tt => tt.CurrentCenter.CenterName == center.CenterName))
                trucks.Add(prod);
            return trucks;

        }
        public DbSet GetDBTruckSlots()
        {
            return db_context.TruckSlots;
        }
        //Наименование на складе
        public ObservableCollection<ProductPosition> GetDBCenterProductsPosition(Center center)
        {
            try 
            { 
            ObservableCollection<ProductPosition> products = new ObservableCollection<ProductPosition>();
            foreach (ProductPosition prod in db_context.ProductPositions.Include(p => p.productCenter).Include(p1 => p1.product).Where(pp => pp.productCenter.CenterName == center.CenterName))
                products.Add(prod);
            return products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void AddProductPositionInDB(ProductPosition product)
        {
            try { 
            db_context.ProductPositions.Add(product);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void UpdateProductPositionInDB(ProductPosition product)
        {
            try 
            { 
            ProductPosition prod = db_context.ProductPositions.Find(product.Id);
            prod.ChangeProperties(product);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DeleteProductPositionFromDB(ProductPosition product)
        {
            try
            {
                ProductPosition prod = db_context.ProductPositions.Find(product.Id);
                if (prod != null)
                {
                    db_context.ProductPositions.Remove(prod);
                    db_context.SaveChanges();
                }
                else MessageBox.Show("Товар не найден на складе");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
