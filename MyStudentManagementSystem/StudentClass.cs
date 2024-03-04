using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyStudentManagementSystem
{
    public class StudentClass
    {
        public int Index { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Date_of_Birth { get; set; }
        public string Address { get; set; }

        public List<ModuleClass> Modules = new List<ModuleClass>();
        public StudentClass(int index,string first_Name, string last_Name, string date_of_Birth, string address)
        {
            Index = index;
            First_Name = first_Name;
            Last_Name = last_Name;
            Date_of_Birth = date_of_Birth;
            Address = address;
        }
        public double GPACalculation()
        {
            double grade_values = 0;
            double Ctotal = 0;
            foreach (ModuleClass module in Modules)
            {
                Ctotal += module.CreditValue;
                grade_values += (module.GradeValue * module.CreditValue);
            }
            double GPA_value = 0;
            try
            {
                GPA_value = grade_values / Ctotal;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("-");
            }
            return GPA_value;
        }
    }
}
