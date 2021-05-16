using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public class DataBase : DbContext
    {
        public DataBase()  : base("EntityConnection")
        {    

        }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Require> Requires { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckSlot> TruckSlots { get; set; }     
        public DbSet<ProductPosition> ProductPositions { get; set; }

        
    }
}
