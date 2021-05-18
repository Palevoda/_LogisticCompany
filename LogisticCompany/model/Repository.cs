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
            ObservableCollection<Center> centers = new ObservableCollection<Center>();
            foreach (Center center in db_context.Centers)
                centers.Add(center);
            return centers;
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
            Center center = GetDBCenters().Where(c => c.CenterName.Equals(employee.center.CenterName)).FirstOrDefault();
            employee.center = center;
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
            Require require = db_context.Requires.Find(req.Id);
            require.Number = req.Number;
            db_context.SaveChanges();
        }
        public void AddRequierInDB(Require require)
        {
            Product product = GetDBProducts().Where(p => p.Id == require.product.Id).FirstOrDefault();
            Center CenterTo = GetDBCenters().Where(c=>c.Id == require.ToCenter.Id).FirstOrDefault();
            Center CenterFrom = GetDBCenters().Where(c => c.Id == require.FromCenter.Id).FirstOrDefault();
            db_context.Requires.Add(new Require(require.Number, product, CenterTo, CenterFrom));
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
            db_context.Trucks.Add(truck);
            db_context.SaveChanges();
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

        public void RemoveTruck(Truck truck)
        {           
            Truck tr = db_context.Trucks.Find(truck.Id);
            db_context.Trucks.Remove(tr);
            db_context.SaveChanges();
        }

        public void UpdateTruck(Truck trck)
        {
            Truck truck = db_context.Trucks.Find(trck.Id);
            truck.fuel_consumption = trck.fuel_consumption;
            truck.if_busy = trck.if_busy;
            Center center = db_context.Centers.Find(trck.CurrentCenter.Id);
            truck.CurrentCenter = center;
            db_context.SaveChanges();
        }

        public void AddTripInDB(Trip trip)
        {
            Center to = db_context.Centers.Where(c => c.Id == trip.To.Id).FirstOrDefault();
            Center from = db_context.Centers.Where(c => c.Id == trip.From.Id).FirstOrDefault();

            trip.To = to;
            trip.From = from;

            db_context.Trips.Add(trip);
            db_context.SaveChanges();
        }

        public void UpdateTrip(Trip trp)
        {
            Trip trip = db_context.Trips.Find(trp.Id);
            trip.Status = trp.Status;
            db_context.SaveChanges();
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

        public void AddTruckSlotInDB(TruckSlot slot)
        {
            Center to = db_context.Centers.Where(c => c.CenterName.Equals(slot.ToCenter.CenterName)).FirstOrDefault();
            Center from = db_context.Centers.Where(c=>c.CenterName.Equals(slot.FromCenter.CenterName)).FirstOrDefault();
            Product prod = db_context.Products.Where(p => p.Name.Equals(slot.product.Name)).FirstOrDefault();
            Trip trip = db_context.Trips.Where(t => t.Id == slot.Trip.Id).Include(t => t.To).Include(tt => tt.From).Include(ttt => ttt.truck).FirstOrDefault();


            slot.product = prod;
            slot.FromCenter = from;
            slot.ToCenter = to;
            slot.Trip = trip;

            db_context.TruckSlots.Add(slot);
            db_context.SaveChanges();
        }

        public Trip GetTripForSlots(Center center)
        {
            Trip trip = db_context.Trips.Where(t => t.From.Id == center.Id && t.Status.Equals("ожидает отправки")).FirstOrDefault();
            return trip;
        }

        public ObservableCollection<TruckSlot> GetSlotsForTrip(Trip trip)
        {
            ObservableCollection<TruckSlot> slots = new ObservableCollection<TruckSlot>();
            foreach (TruckSlot slot in db_context.TruckSlots.Where(t => t.Trip.Id == trip.Id).Include(tt=>tt.FromCenter).Include(ttt=>ttt.ToCenter).Include(t4=>t4.product).Include(t5=>t5.Trip))
            {
                slots.Add(slot);
            }
            return slots;
        }
    }
}
