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
        public ObservableCollection<Center> GetDBCenters()
        {
            try
            {
                ObservableCollection<Center> centers = new ObservableCollection<Center>();
                foreach (Center center in db_context.Centers)
                    centers.Add(center);
                return centers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
            
            
        }
        public void AddCenterInDB(Center center)
        {
            try { 
            db_context.Centers.Add(center);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }
        //Сотрудники
        public ObservableCollection<Employee> GetDBEmployees()
        {
            try
            { 
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            foreach (Employee employee in db_context.Employees.Include(e => e.center))
                employees.Add(employee);
            return employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }
        public Employee GetDBEmployee(string sorname, string phone)//+
        {
            try
            { 
            Employee employee = db_context.Employees.Where(e => e.Sorname.Equals(sorname) && e.phoneNumber.Equals(phone)).Include(ee => ee.center).FirstOrDefault();
            if (employee != null) return employee;
            else return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }
        public void AddEmployeeInDB(Employee employee)
        {
            try
            { 
            Center center = GetDBCenters().Where(c => c.CenterName.Equals(employee.center.CenterName)).FirstOrDefault();
            employee.center = center;
            db_context.Employees.Add(employee);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }
        public Employee FindEmployeeById(int Id)
        {
            try { 
            Employee employee = db_context.Employees.Find(Id);
            return employee;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }
        public void DeleteEmployee(int Id)
        {
            try
            {
                Employee employee = db_context.Employees.Find(Id);
                //if (employee.role.Equals("Администратор") || employee.role.Equals("Admin"))
                //    throw new Exception("Администратор не может удалять других администраторов");
                db_context.Employees.Remove(employee);
                db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Продукты
        public ObservableCollection<Product> GetDBProducts()
        {
            try
            { 
            ObservableCollection<Product> products = new ObservableCollection<Product>();
              foreach (Product product in db_context.Products)
                    products.Add(product); 
            return products;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }
        public void AddProductInDB(Product product)
        {
            try
            { 
            db_context.Products.Add(product);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }
        public void UpdateProductInDataBase(Product product)
        {
            try
            { 
            Product prod = db_context.Products.Find(product.Id);
            prod.ChangeProperties(product);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }
        //Заказы
        public ObservableCollection<Require> GetDBRequiers()
        {
            try
            { 
            ObservableCollection<Require> requiers = new ObservableCollection<Require>();
            foreach (Require requier in db_context.Requires.Include(r => r.product).Include(rr => rr.FromCenter).Include(rrr => rrr.ToCenter))
                requiers.Add(requier);
            return requiers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }
        public ObservableCollection<Require> GetDBRequiersFrom(Center center)
        {
            try
            { 
            ObservableCollection<Require> requiers = new ObservableCollection<Require>();
            foreach (Require requier in db_context.Requires.Include(r => r.product).Include(rr => rr.FromCenter).Include(rrr => rrr.ToCenter).Where(req => req.FromCenter.CenterName.Equals(center.CenterName)))
                requiers.Add(requier);
            return requiers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }
        public ObservableCollection<Require> GetDBRequiersTo(Center center)
        {
            try
            { 
            ObservableCollection<Require> requiers = new ObservableCollection<Require>();
            foreach (Require requier in db_context.Requires.Include(r => r.product).Include(rr => rr.FromCenter).Include(rrr => rrr.ToCenter).Where(req => req.ToCenter.CenterName.Equals(center.CenterName)))
                requiers.Add(requier);
            return requiers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }
        public void DelateRequier(int id)
        {
            try
            {
                Require requier = db_context.Requires.Find(id);
                db_context.Requires.Remove(requier);
                db_context.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void UpdateRequier(Require req)
        {
            try
            { 
            Require require = db_context.Requires.Find(req.Id);
            require.Number = req.Number;
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }
        public void AddRequierInDB(Require require)
        {
            try
            { 
            Product product = GetDBProducts().Where(p => p.Id == require.product.Id).FirstOrDefault();
            Center CenterTo = GetDBCenters().Where(c=>c.Id == require.ToCenter.Id).FirstOrDefault();
            Center CenterFrom = GetDBCenters().Where(c => c.Id == require.FromCenter.Id).FirstOrDefault();
            db_context.Requires.Add(new Require(require.Number, product, CenterTo, CenterFrom));
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
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
            try
            { 
            ObservableCollection<Truck> trucks = new ObservableCollection<Truck>();
            foreach (Truck prod in db_context.Trucks.Include(t=>t.CurrentCenter).Where(tt => tt.CurrentCenter.CenterName == center.CenterName))
                trucks.Add(prod);
            return trucks;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }

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
        public ObservableCollection<ProductPosition> GetAllProductsPosition()
        {
            try
            {
                ObservableCollection<ProductPosition> products = new ObservableCollection<ProductPosition>();
                foreach (ProductPosition prod in db_context.ProductPositions.Include(p => p.productCenter).Include(p1 => p1.product))
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
            try 
            {
            Center center = GetDBCenters().Where(c => c.CenterName.Equals(product.productCenter.CenterName)).FirstOrDefault();
            product.productCenter = center;
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

        public void AddTruck(Truck truck)
        {
            try
            { 
            db_context.Trucks.Add(truck);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }

        public ObservableCollection<Truck> GetTrucks(Center center)
        {
            try
            {
                ObservableCollection<Truck> trucks = new ObservableCollection<Truck>();
                foreach (Truck truck in db_context.Trucks.Include(t => t.CurrentCenter).Where(tt => tt.CurrentCenter.Id == center.Id))
                    trucks.Add(truck);
                return trucks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Truck> GetTrucks()
        {
            try
            {
                ObservableCollection<Truck> trucks = new ObservableCollection<Truck>();
                foreach (Truck truck in db_context.Trucks.Include(t => t.CurrentCenter))
                    trucks.Add(truck);
                return trucks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void RemoveTruck(Truck truck)
        {
            try
            {
                Truck tr = db_context.Trucks.Find(truck.Id);
                db_context.Trucks.Remove(tr);
                db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateTruck(Truck trck)
        {
            try
            {
                Truck truck = db_context.Trucks.Find(trck.Id);
                truck.fuel_consumption = trck.fuel_consumption;
                truck.if_busy = trck.if_busy;
             // truck.CurrentCenter = trck.CurrentCenter;
             // if (trck.CurrentCenter != null)
             // {
                    Center center = db_context.Centers.Find(trck.CurrentCenter.Id);
                    truck.CurrentCenter = center;
             //}
                db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }

        }

        public void AddTripInDB(Trip trip)
        {
            try
            { 
            Center to = db_context.Centers.Where(c => c.Id == trip.To.Id).FirstOrDefault();
            Center from = db_context.Centers.Where(c => c.Id == trip.From.Id).FirstOrDefault();

            trip.To = to;
            trip.From = from;

            db_context.Trips.Add(trip);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }

        public void UpdateTrip(Trip trp)
        {
            try
            { 
            Trip trip = db_context.Trips.Find(trp.Id);
                if (!trip.Status.Equals(trp.Status))
            trip.Status = trp.Status;
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }

        public ObservableCollection<Trip> GetTrips(Center center)
        {
            try
            {
                ObservableCollection<Trip> trips = new ObservableCollection<Trip>();
                foreach (Trip trip in db_context.Trips.Include(t => t.To).Include(tt => tt.From).Include(ttt => ttt.truck).Where(c => c.To.Id == center.Id || c.From.Id == center.Id))
                {
                    trips.Add(trip);
                }
                return trips;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Trip> GetTrips()
        {
            try
            {
                ObservableCollection<Trip> trips = new ObservableCollection<Trip>();
                foreach (Trip trip in db_context.Trips.Include(t => t.To).Include(tt => tt.From).Include(ttt => ttt.truck))
                {
                    trips.Add(trip);
                }
                return trips;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void AddTruckSlotInDB(TruckSlot slot)
        {
            try
            {
                Center to = db_context.Centers.Where(c => c.CenterName.Equals(slot.ToCenter.CenterName)).FirstOrDefault();
                Center from = db_context.Centers.Where(c => c.CenterName.Equals(slot.FromCenter.CenterName)).FirstOrDefault();
                Product prod = db_context.Products.Where(p => p.Name.Equals(slot.product.Name)).FirstOrDefault();
                Trip trip = db_context.Trips.Where(t => t.Id == slot.Trip.Id).Include(t => t.To).Include(tt => tt.From).Include(ttt => ttt.truck).FirstOrDefault();


                slot.product = prod;
                slot.FromCenter = from;
                slot.ToCenter = to;
                slot.Trip = trip;

                db_context.TruckSlots.Add(slot);
                db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }

        public Trip GetTripForSlots(Center center)
        {
            try
            { 
            Trip trip = db_context.Trips.Where(t => t.From.Id == center.Id && t.Status.Equals("ожидает отправки")).FirstOrDefault();
            return trip;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }

        public ObservableCollection<TruckSlot> GetSlotsForTrip(Trip trip)
        {
            try
            { 
            ObservableCollection<TruckSlot> slots = new ObservableCollection<TruckSlot>();
            foreach (TruckSlot slot in db_context.TruckSlots.Where(t => t.Trip.Id == trip.Id).Include(tt=>tt.FromCenter).Include(ttt=>ttt.ToCenter).Include(t4=>t4.product).Include(t5=>t5.Trip))
            {
                slots.Add(slot);
            }
            return slots;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
                return null;
            }
        }
        public void DelateTripsSlots(int trip_id)
        {
            try
            {
                int count_1 = db_context.TruckSlots.Count();
                foreach (TruckSlot slot in db_context.TruckSlots.Where(t => t.Trip.Id == trip_id))
                db_context.TruckSlots.Remove(slot);
                int count_2 = db_context.TruckSlots.Count();
                if (count_1!= count_2)
                db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }

        }

        public void DelateTrip(int trip_id)
        {
            try
            { 
            Trip trip = (Trip)db_context.Trips.Where(t => t.Id == trip_id).FirstOrDefault();
            db_context.Trips.Remove(trip);
            db_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на уровне взаимодействия с базой данных: " + ex.Message);
            }
        }
    }
}
