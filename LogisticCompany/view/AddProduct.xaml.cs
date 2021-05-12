using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogisticCompany.model;
using LogisticCompany.ViewModel;

namespace LogisticCompany.view
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : UserControl
    {
        static AddProduct State;
        ICommand command;
        Employee employee;

        public static AddProduct GetInstance(Employee employee)
        {
            if (State == null)
                State = new AddProduct(employee);
            return State;
        }
        AddProduct(Employee employee)
        {     
            this.employee = employee;
            InitializeComponent();
        }

        private void AddProductInDataBase_Click(object sender, RoutedEventArgs e)
        {
            command = new AddProductCommand(
                employee,
                Convert.ToString(AddProductNameArea.Text),
                Convert.ToString(AddProductLengthArea.Text),
                Convert.ToString(AddProductWidthArea.Text),
                Convert.ToString(AddProductHeightArea.Text),
                Convert.ToString(AddProductWeightArea.Text),
                Convert.ToString(AddProductCostArea.Text),
                Convert.ToString(AddProductUnitArea.Text),
                Convert.ToString(AddProductMinNumArea.Text),
                Convert.ToString(AddProductNumArea.Text)
                );
            if (command.ifExecute()) command.Execute();
        }
    }
}
