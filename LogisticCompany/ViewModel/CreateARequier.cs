using LogisticCompany.model;
using LogisticCompany.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogisticCompany.ViewModel
{
    class CreateARequier : ICommand
    {

        List<string> Centers = new List<string>() { "MinskOne", "GrodnoGodno", "Beresta", "Vitba" };
      //  int[,] Matrix = { { 0, 276, 351, 289 }, { 276, 0, 235, 565 }, { 351, 235, 0, 632 }, { 289, 565, 632, 0 } };
        int[][] Matr;
        Center CenterTo;
        Center CenterFrom;
        ObservableCollection<Center> CentersObjects;
        ObservableCollection<Center> CentersWithProducts;
        Require requier;
        IRepController controller;
        public CreateARequier(Product prod, Center ToCenter, int number)
        {
            try
            {
                CenterTo = ToCenter;
                Matr = new int[Centers.Count][];
                Matr[0] = new int[] { 0, 276, 351, 289 };
                Matr[1] = new int[] { 276, 0, 235, 565 };
                Matr[2] = new int[] { 351, 235, 0, 632 };
                Matr[3] = new int[] { 289, 565, 632, 0 };
                controller = new RepositoryController();
                CentersObjects = controller.GetDBCenters();
                CentersObjects.Remove(ToCenter);
                CentersWithProducts = new ObservableCollection<Center>();
                ProductPosition position;
                foreach (Center center in CentersObjects)
                {
                    position = controller.GetDBCenterProductsPosition(center).Where(p => p.product.Id == prod.Id).Where(pp => pp.numberOfProduct >= number).FirstOrDefault();
                    if (position != null && center.CenterName != ToCenter.CenterName) CentersWithProducts.Add(center);
                }

                if (CentersWithProducts.Count != 0)
                {
                    CenterFrom = CentersWithProducts[0];
                    for (int i = 0; i < CentersWithProducts.Count; i++)
                    {
                        if (GetDistance(CentersWithProducts[i]) < GetDistance(CenterFrom))
                            CenterFrom = CentersWithProducts[i];
                    }
                }
                else throw new Exception("Ни в одном центре нет такого количества товаров");

                //Создать заказ и отправить в БД
                requier = new Require(number, prod, CenterTo, CenterFrom);
                OpenCloseOrder.GetInstance(EmployeeWindow.GetInstance().employe).Requiers.Add(requier);
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Execute()
        {
            controller.AddRequierInDB(requier);
        }

        public bool ifExecute()
        {
            if (requier != null) return true;
            else return false;
        }

        public Require GetRequier()
        {
            return requier;
        }

        public int GetCenterIndex(Center center)
        {
            for (int i = 0; i < Centers.Count; i++)
                if (Centers[i].Equals(Centers[i]))
                    return i;

            return -1;            
        }
        public void GetCenterWithProduct()
        {
            
        }
        public int GetDistance(Center center_from)
        {
            int id_from = GetCenterIndex(center_from);
            int id_to = GetCenterIndex(CenterTo);

            return Matr[id_from][id_to];
        }
        public string GetClosestCenter()
        {
            int distance;
            int to_center_id = GetCenterIndex(CenterTo);
            if (to_center_id == -1) throw new Exception("Хуево дан центр");

            foreach (int i in Matr[to_center_id])
            {
                
            }
            for (int i = 0; i < Matr[to_center_id].Length-1; i++)
            {
                distance = Matr[to_center_id][i];
                if (distance > Matr[to_center_id][i + 1])
                    distance = Matr[to_center_id][i + 1];
            }

            return "";
        }
    }
}
