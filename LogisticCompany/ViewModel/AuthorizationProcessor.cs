using LogisticCompany.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;
using LogisticCompany.model;

namespace LogisticCompany.ViewModel
{
    class AuthorizationProcessor : ICommand
    {
        string sorname;
        string phone;
        string user_password;
        public AuthorizationProcessor(string sorn, string phon, string password)
        {
            sorname = sorn;
            phone = phon;
            user_password = password;
        }

        public void Execute()
        {
            try
            {
                var md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(user_password));
                string password = "";
                foreach (byte bit in hash)
                    password += bit;

                LogisticCompany.model.IRepController repository = new LogisticCompany.model.RepositoryController();
                Employee employee = repository.GetEmployeeFromDB(sorname, phone);
                if (employee != null)
                {
                    if (employee.password.Equals(password))
                    {
                        //MessageBox.Show("Пользователь идентифицирован");
                        LogisticCompany.MainWindow.GetInstance().Content = LogisticCompany.view.EmployeeWindow.GetInstance(employee).Content;
                    }
                    else MessageBox.Show("Введены неверные данные\t");
                }
                else throw new Exception("Пользователь не найден");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool ifExecute()
        {
            if (sorname.Length > 0 && phone.Length > 0 && user_password.Length > 0)
                return true;
            else
            {
                MessageBox.Show("Форма авторизации содержит пустые поля");
                return false;
            }
        }

    }
}
