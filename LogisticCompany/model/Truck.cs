using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public class Truck : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public float fuel_consumption { get; set; }
        public int slots { get; set; }
        public int load_capacity { get; set; }
        public int volume_capcity { get; set; }

        private bool ifBusy; 
        public bool if_busy
        {
            get { return ifBusy; }
            set { ifBusy = value; OnPropertyChanged("ifBusy"); }
        }
        private Center currentCenter;
        public Center CurrentCenter
        {
            get { return currentCenter; }
            set {
                currentCenter = value; 
                OnPropertyChanged("CurrentCenter"); 
            }
        }


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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                IRepController controller = new RepositoryController();
                controller.UpdateTruck(this);
            }
        }
    }
}
