using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public interface IRepController
    {
        Employee GetEmployeeFromDB(string sorname, string phone);
        ObservableCollection<ProductPosition> GetStorageProducts(Center center);
        ObservableCollection<Require> GetDBRequiersFrom(Center center);
        ObservableCollection<Require> GetDBRequiersTo(Center center);
        ObservableCollection<ProductPosition> GetDBCenterProductsPosition(Center center);
        void UpdateProductInDataBase(Product product);
        void UpdateProductPositionInDB(ProductPosition product);
        void DeleteProductPositionFromDB(ProductPosition product);
        void AddProductPositionInDB(ProductPosition product);
        void AddProductInDB(Product product);
        ObservableCollection<Product> GetDBProducts();
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
    }
}
