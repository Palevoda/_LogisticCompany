using LogisticCompany.model;
using LogisticCompany.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LogisticCompany.ViewModel
{
    class AddProductCommand : ICommand
    {
        string Name;
        string Length;
        string Width;
        string Height;
        string Weight;
        string Cost;
        string Unit;
        string Min_num;
        string Number;
        Employee employee;
        IRepController controller = new RepositoryController();

        public AddProductCommand(Employee empl, string name, string length, string width, string height, string weight, string cost, string unit, string min_num, string number)
        {
            Name = name;
            Length = length;
            Width = width;
            Height = height;
            Weight = weight;
            Cost = cost;
            Unit = unit;
            Min_num = min_num;
            Number = number;
            employee = empl;
        }

        public void Execute()
        {
            Product product = new Product(
                Name,
                (float)Convert.ToDouble(Length),
                (float)Convert.ToDouble(Width),
                (float)Convert.ToDouble(Height),
                (float)Convert.ToDouble(Cost),
                (float)Convert.ToDouble(Weight),
                Convert.ToInt32(Min_num),
                Unit
                ); ;
            ProductPosition productPosition = new ProductPosition(product, employee.center, Convert.ToInt32(Number));
            controller.AddProductPositionInDB(productPosition);
            MessageBox.Show("Товар добавлен");
            //controller.AddProductInDB(product);

        }

        public bool ifExecute()
        {
            return true;
        }

    }
}
