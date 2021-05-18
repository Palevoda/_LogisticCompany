using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LogisticCompany.model
{
    public class TruckSlot
    {
        IRepController controller = new RepositoryController();
        static int MAXVOLUME = 1200000000;
        public int Id { get; set; }
        public int total_umber { get; set; }
        public float total_weight { get; set; }
        public float total_cost { get; set; }
        public float slot_max_volume { get; set; }
        public Product product { get; set; }
        public Center ToCenter { get; set; }
        public Center FromCenter { get; set; }
        public Trip Trip { get; set; }
        public TruckSlot() { }
        TruckSlot(Product prod, Center To, Center From, int number)
        {
            product = prod;
            ToCenter = To;
            FromCenter = From;
            slot_max_volume = MAXVOLUME;
            total_umber = number;
            total_cost = number * prod.cost;
            total_weight = number * prod.weight;
        }

        public void SetTrip(Trip trip)
        {
            Trip = trip;
        }

        public void AddInDB()
        {
            controller.AddTruckSlotInDB(this);
        }
        
        static int CountMaxNumberOfProductsByVolume(Product product)
        {
            return Convert.ToInt32(MAXVOLUME / (product.length*product.weight*product.height));
        }
        static int GetNumberOfSlotsInRequier(int num, int MaxNumber)
        {
            float number = (float)Convert.ToDouble(num);
            float max_num_in_slot = (float)Convert.ToDouble(MaxNumber);

            if (number / max_num_in_slot <= 1)
                return 1;
            else return Convert.ToInt32(num/MaxNumber+1);
        }
        static int GetNumberInSlot(int NumberOfSlots, int numberOfProducts, int number)
        {
            if (NumberOfSlots == 1) return number;
            else
            {
                int number_in_slot = Convert.ToInt32(numberOfProducts / NumberOfSlots);
                return number_in_slot;
            }
        }
        public static ObservableCollection<TruckSlot> GetSlotsFromRequier(Require requier)
        {
            ObservableCollection<TruckSlot> slots = new ObservableCollection<TruckSlot>();

            int max_number_of_products_in_slot = CountMaxNumberOfProductsByVolume(requier.product);
            int number_of_slots_in_requier = GetNumberOfSlotsInRequier(requier.Number, max_number_of_products_in_slot);
            int number_in_slot = GetNumberInSlot(number_of_slots_in_requier, max_number_of_products_in_slot, requier.Number);

            for (int i = 0; i < number_of_slots_in_requier; i++)
            {
                slots.Add(new TruckSlot(
                        requier.product,
                        requier.ToCenter,
                        requier.FromCenter,
                        requier.Number
                    ));
            }
            return slots;
        }
    }
}
