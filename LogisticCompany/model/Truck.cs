using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public class Truck
    {
        public string Id { get; set; }
        public float fuel_consumption { get; set; }
        public int slots { get; set; }
        public int load_capacity { get; set; }
        public int volume_capcity { get; set; }
        public bool if_busy { get; set; }
        public Center CurrentCenter { get; set; }
        public Truck() { }

        public Truck(string reg, float cnsmpshn, int slots, int load_cap, int volume_cap, bool busy) 
        {
            Id = reg;
            fuel_consumption = cnsmpshn;
            this.slots = slots;
            load_capacity = load_cap;
            volume_capcity = volume_cap;
            if_busy = busy;
        }


    }
}
