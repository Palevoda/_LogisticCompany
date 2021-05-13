using LogisticCompany.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.ViewModel
{
    class CreateARequier
    {

        List<string> Cities = new List<string>() { "MinskOne", "GrodnoGodno", "Beresta", "Vitba" };
        public CreateARequier(Product prod, Center ToCenter, int number)
        {
            
        }

        public string GetClosestCenter()
        {
            int[,] Matrix = new[,] { { 0, 276, 351, 289 }, { 276, 0, 235, 565 }, { 351, 235, 0, 632}, {289, 565, 632, 0 } };
            return "";
        }
    }
}
