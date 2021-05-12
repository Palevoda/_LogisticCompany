using LogisticCompany.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class Authorization : UserControl
    {
        ICommand command;
        static Authorization State;
        public static Authorization GetInstance()
        {
            if (State == null)
                State = new Authorization();
            return State;
        }
        public Authorization()
        {
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            command = new LogisticCompany.ViewModel.AuthorizationProcessor(
                Convert.ToString(AuthSorname.Text), 
                Convert.ToString(AuthPhone.Text),
                Convert.ToString(AuthPassword.Password)
                );

            if (command.ifExecute())
                command.Execute();          
        }
    }
}
