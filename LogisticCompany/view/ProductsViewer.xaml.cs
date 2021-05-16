using LogisticCompany.model;
using LogisticCompany.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LogisticCompany.view
{
    /// <summary>
    /// Логика взаимодействия для ProductsViewer.xaml
    /// </summary>
    /// 
    


    public partial class ProductsViewer : UserControl
    {
        public static ObservableCollection<ViewerObject> ProductsOnStorage;
        static ProductsViewer State;
        Employee employee;
        ICommand GetNewContext;
        
        public static ProductsViewer GetInstance(Employee empl)
        {
            if (State == null)
                State = new ProductsViewer(empl);

            ProductsOnStorage.Clear();
            if (State.GetNewContext.ifExecute()) State.GetNewContext.Execute();
            return State;
        }
        ProductsViewer(Employee empl)
        {

            ///  Repository rr = new Repository();
            //  Product prod = Product.GenerateRandomProduct();
            // rr.AddProductInDB(prod);
            //rr.AddProductPositionInDB(ProductPosition.GenerateRandomProductPosition(prod, empl.center));
            // Require.GenerateRequiersInDB(empl.center, empl.center, 50);

            ProductsOnStorage = new ObservableCollection<ViewerObject>();
            employee = empl;
            GetNewContext = new SetProductViewer();
           // if (GetNewContext.ifExecute()) GetNewContext.Execute();
            InitializeComponent();
            ProductsViewerTable.ItemsSource = ProductsOnStorage;
        }

    }
}
