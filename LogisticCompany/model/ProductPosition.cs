using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public class ProductPosition : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Product _product;
        public Product product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged("_product"); }
        }
        public Center productCenter { get; set; }
        public int NumberOfProduct;

        public int numberOfProduct
        {
            get { return NumberOfProduct; }
            set { NumberOfProduct = value; OnPropertyChanged("NumberOfProduct"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                IRepController controller = new RepositoryController();
                controller.UpdateProductPositionInDB(this);
            }

        }

        public ProductPosition() { }
        public ProductPosition(Product prod, Center cent, int num) 
        {
            product = prod;
            productCenter = cent;
            NumberOfProduct = num;
        }

        public void ChangeProperties(ProductPosition product)
        {
            NumberOfProduct = product.NumberOfProduct;
        }

        public static ProductPosition GenerateRandomProductPosition(Product prod, Center cent)
        {
            Random rand = new Random();
            return new ProductPosition(prod, cent, rand.Next(46));
        }
    }
}
