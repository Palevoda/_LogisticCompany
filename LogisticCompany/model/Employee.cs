using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace LogisticCompany.model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Sorname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }        
        public string role { get; set; }
        public string password { get; set; } 
        public string phoneNumber { get; set; }
        public Center center { get; set; }
        public Employee() { }
        public Employee(string sorname, string name, string second_name, string role, string password, string phone, Center center) 
        {
            Sorname = sorname;
            Name = name;
            SecondName = second_name;

            if (role.Equals("Admin") || role.Equals("Employee") || role.Equals("Администратор") || role.Equals("Сотрудник"))
                this.role = role;
            else throw new Exception("Неверно указан статус. Допустимые значение: Admin/Employee/Администратор/Сотрудник");
            //Шифрование
            var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            string result = "";
            foreach (byte bit in hash)
                result += bit;           
            this.password = result;
            //
            this.center = center;
            phoneNumber = phone;
        }
    }
}
