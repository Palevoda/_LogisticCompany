using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public class Trip
    {
        public ObservableCollection<TruckSlot> Slots = new ObservableCollection<TruckSlot>();
        public int Id { get; set; }
        public Truck truck { get; set; }
        public Center From { get; set; }
        public Center To { get; set; }
        public string Status { get; set; } // template/waiting/intrip/complated
        public Trip() { }
        
    }
}
