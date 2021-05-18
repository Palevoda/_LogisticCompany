using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LogisticCompany.model
{
    public class Trip : INotifyPropertyChanged
    {
        public ObservableCollection<TruckSlot> Slots = new ObservableCollection<TruckSlot>();
        IRepController controller = new RepositoryController();
        public int Id { get; set; }
        public Truck truck { get; set; }
        public Center From { get; set; }
        public Center To { get; set; }
        private string status; // template/waiting/intrip/complated

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("status"); }
        }
        public Trip() { }

        public Trip(ObservableCollection<TruckSlot> slots,Center to, Center from, Truck trck, string status) 
        {
            To = to;
            From = from;
            Status = status;
            truck = trck;
            Slots = slots;           
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                IRepController controller = new RepositoryController();
                controller.UpdateTrip(this);
            }
        }

        public void SaveSlots()
        {
            foreach (TruckSlot slot in Slots)
            {
                slot.SetTrip(this);
                controller.AddTruckSlotInDB(slot);
            }
        }
        
    }
}
