using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public interface IRepository
    {
        DataBase GetContext();
        //Получить коллекци
        void AddCenterInDB(Center center);
        //Сотрудники
        Employee GetDBEmployee(string sorname, string phone);
        void AddEmployeeInDB(Employee employee);
        ObservableCollection<Employee> GetDBEmployees();
        ObservableCollection<Product> GetDBProducts();
        void AddProductInDB(Product product);
        void UpdateProductInDataBase(Product product);
        ObservableCollection<Require> GetDBRequiers();
        void AddProductPositionInDB(ProductPosition product);
        DbSet GetDBTrips();
        DbSet GetDBTrucks();
        DbSet GetDBTruckSlots();
        ObservableCollection<ProductPosition> GetDBCenterProductsPosition(Center center);
        ObservableCollection<Require> GetDBRequiersFrom(Center center);
        ObservableCollection<Require> GetDBRequiersTo(Center center);
        void UpdateProductPositionInDB(ProductPosition product);
        void DeleteProductPositionFromDB(ProductPosition product);
        ObservableCollection<Center> GetDBCenters();
        void AddRequierInDB(Require require);
        void DelateRequier(int id);
        ObservableCollection<Truck> GetTrucks(Center center);
        void AddTruck(Truck truck);
        void RemoveTruck(Truck truck);
        void AddTripInDB(Trip trip);
        ObservableCollection<Trip> GetTrips(Center center);
        void AddTruckSlotInDB(TruckSlot slot);
        Trip GetTripForSlots(Center center);
        ObservableCollection<TruckSlot> GetSlotsForTrip(Trip trip);
        void UpdateTruck(Truck trck);
        void UpdateRequier(Require req);
        void UpdateTrip(Trip trp);
        ObservableCollection<ProductPosition> GetAllProductsPosition();
        ObservableCollection<Truck> GetTrucks();
        ObservableCollection<Trip> GetTrips();
        void DelateTripsSlots(int trip_id);
        void DelateTrip(int trip_id);
        void DeleteEmployee(int Id);
        Employee FindEmployeeById(int Id);
    }
}
