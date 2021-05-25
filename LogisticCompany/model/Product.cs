using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogisticCompany.model
{
    public class Product : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("name"); }
        }

        public float length;
        public float Length
        {
            get { return length; }
            set { length = value; OnPropertyChanged("length"); }
        }
        public float width;
        public float Width
        {
            get { return width; }
            set { width = value; OnPropertyChanged("width"); }
        }

        public float height;
        public float Height
        {
            get { return height; }
            set { height = value; OnPropertyChanged("height"); }
        }

        public float cost;
        public float Cost
        {
            get { return cost; }
            set { cost = value; OnPropertyChanged("cost"); }
        }

        public float weight;
        public float Weight
        {
            get { return weight; }
            set { weight = value; OnPropertyChanged("weight"); }
        }

        public int mominal_number;
        public int Mominal_number
        {
            get { return mominal_number; }
            set { mominal_number = value; OnPropertyChanged("mominal_number"); }
        }

        public string unit_of_measurment;

        public string Unit_of_measurment
        {
            get { return unit_of_measurment; }
            set { unit_of_measurment = value; OnPropertyChanged("unit_of_measurment"); }
        }

        public Product() { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                IRepController controller = new RepositoryController();
                controller.UpdateProductInDataBase(this);
            }
        }

        public Product(string name, float len, float wid, float heigh, float cst, float weigh, int nom_numb, string unit_of_meas)
        {
            this.name = name;
            length = len;
            width = wid;
            height = heigh;
            cost = cst;
            weight = weigh;
            mominal_number = nom_numb;
            unit_of_measurment = unit_of_meas;
        }

        public void ChangeProperties(Product New)
        {
            name = New.name;
            length = New.length;
            width = New.width;
            height = New.height;
            cost = New.cost;
            weight = New.weight;
            mominal_number = New.mominal_number;
            unit_of_measurment = New.unit_of_measurment;
        }
        public static Product GenerateRandomProduct()
        {
            Random random = new Random();
            Product product = new Product(
                "ProductX",
                (float)random.Next(1000),
                (float)random.Next(1000),
                (float)random.Next(1000),
                (float)random.Next(100),
                (float)random.Next(40),
                random.Next(25),
                "кг");

            return product;
        }
    }
}
