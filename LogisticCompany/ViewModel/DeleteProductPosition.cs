using LogisticCompany.model;
using LogisticCompany.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogisticCompany.ViewModel
{
    class DeleteProductPosition : ICommand
    {
        ProductsObserver observer;
        public DeleteProductPosition(ProductsObserver observ)
        {
            observer = observ;
        }
        public void Execute()
        {
            try
            {
                int id = Convert.ToInt32(observer.IdForDelateProductPosition.Text);
                ProductPosition prod = ReductProducts.ProductsOnStorage.Where(p => p.Id == id).FirstOrDefault();
                
                    ReductProducts.ProductsOnStorage.Remove(prod);
                    IRepController controller = new RepositoryController();
                    controller.DeleteProductPositionFromDB(prod);
                
            }
            catch (Exception  ex)
            {
                MessageBox.Show("Товар не найден. Проверьте правильность введенного индекса");
            }
        }

        public bool ifExecute()
        {
            return true;
        }
    }
}
