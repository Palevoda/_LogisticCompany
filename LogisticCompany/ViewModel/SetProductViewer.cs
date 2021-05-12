using LogisticCompany.model;
using LogisticCompany.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.ViewModel
{
    class SetProductViewer : ICommand
    {
        public void Execute()
        {

            IRepController repository = new RepositoryController();
            Employee employee = EmployeeWindow.GetInstance().employe;
            ObservableCollection<ProductPosition> products = repository.GetStorageProducts(employee.center);

            foreach (ProductPosition product in products)
                ProductsViewer.ProductsOnStorage.Add(new ViewerObject(product));

            ObservableCollection<Require> requiersTo = repository.GetDBRequiersTo(employee.center);
            ObservableCollection<Require> requiersFrom = repository.GetDBRequiersFrom(employee.center);

            foreach (ViewerObject viewObj in ProductsViewer.ProductsOnStorage)
            {
                viewObj.SetNumberFrom(requiersFrom);
                viewObj.SetNumberTo(requiersTo);
            }
        }

        public bool ifExecute()
        {
            return true;
        }
    }
}
