using LogisticCompany.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogisticCompany.ViewModel
{
    class ExitCommand : ICommand
    {
        public void Execute()
        {
            MainWindow.GetInstance().Content = new LogisticCompany.view.Authorization().Content;
        }

        public bool ifExecute()
        {
            string res = MessageBox.Show("Вы уверены, что хотите выйти?", "",
                    MessageBoxButton.YesNo, MessageBoxImage.Information).ToString();
            if (res.Equals("Yes"))
                return true;
            else return false;
        }
    }
}
