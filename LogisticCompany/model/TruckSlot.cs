using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public class TruckSlot
    {
        public int Id { get; set; }
        public int total_umber { get; set; }
        public float total_weight { get; set; }
        public float total_cost { get; set; }
        public float slot_max_volume { get; set; }
        public Product product { get; set; }
        public Center ToCenter { get; set; }
        public Center FromCenter { get; set; }
        public TruckSlot() { }


    }
}
