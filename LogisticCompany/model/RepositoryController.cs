using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LogisticCompany.model
{
    public class RepositoryController :IRepController
    {
        static RepositoryController controller;
        IRepository repository = new Repository();

        public RepositoryController GetInstance()
        {
            if (controller == null)
                controller = new RepositoryController();
            return controller;
        }
        public RepositoryController() { }

        public Employee GetEmployeeFromDB(string sorname, string phone)
        {
            return repository.GetDBEmployee(sorname, phone);
        }
        public ObservableCollection<ProductPosition> GetStorageProducts(Center center)
        {
            return repository.GetDBCenterProductsPosition(center);
        }
        public void UpdateProductInDataBase(Product product)
        {
            repository.UpdateProductInDataBase(product);
        }
        public ObservableCollection<Require> GetDBRequiersFrom(Center center)
        {
            return repository.GetDBRequiersFrom(center);
        }
        public ObservableCollection<Require> GetDBRequiersTo(Center center)
        {
            return repository.GetDBRequiersTo(center);
        }
        public ObservableCollection<ProductPosition> GetDBCenterProductsPosition(Center center)
        {
            return repository.GetDBCenterProductsPosition(center);
        }

        public void UpdateProductPositionInDB(ProductPosition product)
        {
            repository.UpdateProductPositionInDB(product);
        }

        public void DeleteProductPositionFromDB(ProductPosition product)
        {
            repository.DeleteProductPositionFromDB(product);
        }

        public void AddProductInDB(Product product)
        {
            repository.AddProductInDB(product);
        }

        public void AddProductPositionInDB(ProductPosition product)
        {
            repository.AddProductPositionInDB(product);
        }

        public ObservableCollection<Product> GetDBProducts()
        {
            return repository.GetDBProducts();
        }

        public ObservableCollection<Center> GetDBCenters()
        {
            return repository.GetDBCenters();
        }

        public void AddRequierInDB(Require require)
        {
            repository.AddRequierInDB(require);
        }

        public void DelateRequier(int id)
        {
            repository.DelateRequier(id);
        }
        public ObservableCollection<Truck> GetTrucks(Center center)
        {
            return repository.GetTrucks(center);
        }
        public void AddTruck(Truck truck)
        {
            repository.AddTruck(truck);
        }
        public void RemoveTruck(Truck truck)
        {
            repository.AddTruck(truck);
        }
        public void AddTripInDB(Trip trip)
        {
            repository.AddTripInDB(trip);
        }
        public ObservableCollection<Trip> GetTrips(Center center)
        {
            return repository.GetTrips(center);
        }
        public void AddTruckSlotInDB(TruckSlot slot)
        {
            repository.AddTruckSlotInDB(slot);
        }
        public Trip GetTripForSlots(Center center)
        {
            return repository.GetTripForSlots(center);
        }
        public ObservableCollection<TruckSlot> GetSlotsForTrip(Trip trip)
        {
            return repository.GetSlotsForTrip(trip);
        }
        public void UpdateTruck(Truck trck)
        {
            repository.UpdateTruck(trck);
        }
        public void UpdateRequier(Require req)
        {
            repository.UpdateRequier(req);
        }

        public void UpdateTrip(Trip trp)
        {
            repository.UpdateTrip(trp);
        }
    }

}
