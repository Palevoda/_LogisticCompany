using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public class Center
    {
        public ObservableCollection<ProductPosition> products = new ObservableCollection<ProductPosition>();
        public ObservableCollection<Require> requiers = new ObservableCollection<Require>();
        public ObservableCollection<Truck> trucks_on_parking = new ObservableCollection<Truck>();
        public int Id { get; set; }
        public string CenterName { get; set; }
        public string CenterCity { get; set; }
        public float MaxStorageCapacity { get; set; }
        public Center(){}

        public Center(string name, string city, float max_storage_capacity)
        {
            CenterName = name;
            CenterCity = city;
            MaxStorageCapacity = max_storage_capacity;

            //products = Repository.GetInstance().GetDBCenterProductsPosition(this);
            //trucks_on_parking = Repository.GetInstance().GetDBCenterParking(this);

        }

        public void GetProfuctsFromDB()
        {
            
        }


    }
}
