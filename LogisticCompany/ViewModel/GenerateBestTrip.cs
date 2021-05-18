using LogisticCompany.model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogisticCompany.ViewModel
{
    class ToCenterSlotsCountingInfo
    {
        public ObservableCollection<TruckSlot> Slots;
        public Center ToCenter;
        ObservableCollection<TruckSlot> SlotsResult;
        Truck truck;
        public int cost = 0;
        public int weight = 0;

        public ToCenterSlotsCountingInfo(ObservableCollection<TruckSlot> slots, Center to, Truck trck)
        {
            SlotsResult = new ObservableCollection<TruckSlot>();
            Slots = slots;
            ToCenter = to;
            truck = trck;

            if (Slots.Count <= truck.slots)
            {
                SlotsResult = Slots;
                foreach (TruckSlot slot in SlotsResult)
                {
                    cost += Convert.ToInt32(slot.total_cost);
                    weight += Convert.ToInt32(slot.total_weight);
                }
            }
            else
            {
                var sortedClots = Slots.OrderByDescending(t => t.total_cost).OrderBy(tt=>tt.total_weight);
                foreach (var slot in sortedClots)
                {
                    if (SlotsResult.Count <= truck.slots && weight <= truck.load_capacity && (slot.total_weight+weight) <= truck.load_capacity)
                    {
                        SlotsResult.Add((TruckSlot)slot);
                        cost += Convert.ToInt32(slot.total_cost);
                        weight += Convert.ToInt32(slot.total_weight);
                    }                        
                }  
            }
        }        
    }
    class GenerateBestTrip
    {
        ObservableCollection<ToCenterSlotsCountingInfo> FromCentersTrips;
        ObservableCollection<Require> Requiers;
        ObservableCollection<TruckSlot> Slots;
        ToCenterSlotsCountingInfo result;
        public Truck truck;
        IRepController controller;

        public GenerateBestTrip(ObservableCollection<Require> requiers, Truck trck)
        {
            FromCentersTrips = new ObservableCollection<ToCenterSlotsCountingInfo>();
            Slots = new ObservableCollection<TruckSlot>();
            controller = new RepositoryController();
            Requiers = requiers;
            truck = trck;
            //Execute
            GenerateSlots();
            GenerateTripInfo(trck);
            result = GetTheBetterTrip();
        }

        
        public void GenerateSlots()
        {
            foreach (Require req in Requiers)
            {
                foreach (TruckSlot slot in TruckSlot.GetSlotsFromRequier(req))
                    Slots.Add(slot);
            }
        }
        public void GenerateTripInfo(Truck truck)
        {
            ObservableCollection<Center> centers = controller.GetDBCenters();
            foreach (Center center in centers)
            {
                if (!center.CenterName.Equals(truck.CurrentCenter.CenterName))
                {
                    ObservableCollection<TruckSlot> SlotsTocenter = new ObservableCollection<TruckSlot>();

                    var truckSlots = Slots.Where(s => s.ToCenter.Id == center.Id);
                    foreach (var slot in truckSlots)
                        SlotsTocenter.Add((TruckSlot)slot);

                    FromCentersTrips.Add(new ToCenterSlotsCountingInfo(SlotsTocenter, center, truck));
                }
            }
        }
        public ToCenterSlotsCountingInfo GetTheBetterTrip()
        {
            ToCenterSlotsCountingInfo thebest = FromCentersTrips[0];
            foreach (ToCenterSlotsCountingInfo info in FromCentersTrips)
            {
                if (thebest.cost < info.cost)
                thebest = info;
            }
            return thebest;
        }
    }
}
