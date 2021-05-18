using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public class Require : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int number;
        public int Number
        {
            get { return number; }
            set { number = value; OnPropertyChanged("number"); }
        }
        public Product product { get; set; }
        public Center ToCenter { get; set; }
        public Center FromCenter { get; set; }
        public Require() { }

        public Require(int num, Product prod, Center to, Center from) 
        {
            Number = num;
            product = prod;
            ToCenter = to;
            FromCenter = from;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                IRepController controller = new RepositoryController();
               controller.UpdateRequier(this);
            }
        }

        public static void GenerateRequiersInDB(Center CenterTo, Center CenterFrom, int col)
        {
            Random random = new Random();
            Repository repository = new Repository();
            ObservableCollection<Product> products = repository.GetDBProducts();
            for (int i = 0; i < col; i++)
            repository.AddRequierInDB(
                new Require(
                    random.Next(35),
                    products[random.Next(products.Count - 1)],
                    CenterTo,
                    CenterFrom
                    )
                );
        }
    }
}
