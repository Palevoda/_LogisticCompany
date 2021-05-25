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
    /// Логика взаимодействия для AdminRequiersTable.xaml
    /// </summary>
    public partial class AdminRequiersTable : UserControl
    {
        static AdminRequiersTable State;
        ObservableCollection<Require> requiers;
        IRepController controller = new RepositoryController();
        public static AdminRequiersTable GetInstance()
        {
            if (State == null)
                State = new AdminRequiersTable();
            State.SetContext();
            return State;
        }
        
        public AdminRequiersTable()
        {
            requiers = controller.GetDBRequiers();
            InitializeComponent();
            RequiersTable.ItemsSource = requiers;
        }
        void SetContext()
        {
            requiers.Clear();
            foreach (Require req in controller.GetDBRequiers())
                requiers.Add(req);
        }
    }
}
