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
    /// <summary>
    /// Логика взаимодействия для AdminRequiers.xaml
    /// </summary>
    public partial class AdminRequiers : UserControl
    {
        static AdminRequiers State;
        IRepController controller = new RepositoryController();
        public static AdminRequiers GetInstance()
        {
            if (State == null)
                State = new AdminRequiers();
            return State;
        }
        public AdminRequiers()
        {            
            InitializeComponent();
            RequiersWorkingArea.Content = AdminRequiersTable.GetInstance();

        }
        private void ViewRequier_Click(object sender, RoutedEventArgs e)
        {
            RequiersWorkingArea.Content = AdminRequiersTable.GetInstance();
        }
        private void RemoveRequier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NumberArea.Text == "")
                    throw new Exception("Введи Id заказа, который хотите отменить");
                int Id = Convert.ToInt32(NumberArea.Text);
                controller.DelateRequier(Id);
                AdminRequiersTable.GetInstance();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateRequier_Click(object sender, RoutedEventArgs e)
        {
            RequiersWorkingArea.Content = AdminGenerateRequier.GetInstance();
        }

        
    }
}
