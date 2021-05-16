using LogisticCompany.model;
using LogisticCompany.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.ViewModel
{
    class DelateRequier :ICommand
    {
        int ID;
        public DelateRequier(int id)
        {
            ID = id;
        }
        public void Execute()
        {
            IRepController controller = new RepositoryController();
            controller.DelateRequier(ID);
        }

        public bool ifExecute()
        {
            return true;
        }
    }
}
