using LogisticCompany.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LogisticCompany.view
{   
    public partial class AdminGenerateRequier : UserControl
    {
        static AdminGenerateRequier State;
        ObservableCollection<Center> centers;
        ObservableCollection<Product> products;
        IRepController controller = new RepositoryController();
        public static AdminGenerateRequier GetInstance()
        {
            if (State == null)
                State = new AdminGenerateRequier();
            return State;
        }
        public AdminGenerateRequier()
        {
            try
            {
                centers = controller.GetDBCenters();
                products = controller.GetDBProducts();
                InitializeComponent();
                FromCenter.ItemsSource = centers;
                FromCenter.DisplayMemberPath = "CenterName";
                ToCenter.ItemsSource = centers;
                ToCenter.DisplayMemberPath = "CenterName";
                Products.ItemsSource = products;
                Products.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateRequier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((Center)ToCenter.SelectedItem == null)
                    throw new Exception("Укажите пункт назначения");
                if ((Center)FromCenter.SelectedItem == null)
                    throw new Exception("Укажите пункт отправления");
                if ((Product)Products.SelectedItem == null)
                    throw new Exception("Укажите продукт для заказа");
                if (ReqProductNumber.Text == "")
                    throw new Exception("Укажите количество");

                Center centerTo = (Center)ToCenter.SelectedItem;
                Center centerFrom = (Center)FromCenter.SelectedItem;

                if (centerTo.Id == centerFrom.Id)
                    throw new Exception("Один и тот же склад не может быть пунктом отправки и назначения в рамках одного заказа");

                Product prod = (Product)Products.SelectedItem;
                int number = Convert.ToInt32(ReqProductNumber.Text);

                if (controller.GetDBCenterProductsPosition(centerFrom).Where(p => p.product.Id == prod.Id && p.NumberOfProduct > number).FirstOrDefault() != null)
                {
                    Require requier = new Require(number, prod, centerTo, centerFrom);
                    controller.AddRequierInDB(requier);
                    MessageBox.Show("Добавлен новый заказ");
                }
                else MessageBox.Show("В на выбранном вами складе нет столько продуктов данного наименования");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
