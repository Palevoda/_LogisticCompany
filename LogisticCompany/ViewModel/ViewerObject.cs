using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticCompany.model;

namespace LogisticCompany.ViewModel
{
    public class ViewerObject
    {
        int Id { get; set; }
        public string name { get; set; }
        public int current_number { get; set; }
        public int number_to { get; set; }
        public int number_from { get; set; }

        public ViewerObject(ProductPosition prod)
        {
            Id = prod.product.Id;
            name = prod.product.name;
            current_number = prod.NumberOfProduct;
            number_to = 0;
            number_from = 0;
        }

        public void SetNumberTo(ObservableCollection<Require> requiers)
        {
            foreach (Require require in requiers)
                if (require.product.Id == Id)
                    number_to += require.Number;
        }

        public void SetNumberFrom(ObservableCollection<Require> requiers)
        {
            foreach (Require require in requiers)
                if (require.product.Id == Id)
                    number_from += require.Number;
        }
    }
}
