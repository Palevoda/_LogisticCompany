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
    class GetRequiers : ICommand
    {
        Employee employee;
        IRepController controller;
        public GetRequiers(Employee empl)
        {
            controller = new RepositoryController();
            employee = empl;
        }

        public void Execute()
        {
            //OpenCloseOrder.GetInstance(employee).Requiers.Clear();
            OpenCloseOrder.GetInstance(employee).Requiers.Add(controller.GetDBRequiersTo(employee.center)[controller.GetDBRequiersTo(employee.center).Count-1]);
        }

        public bool ifExecute()
        {
            return true;
        }
    }
}
