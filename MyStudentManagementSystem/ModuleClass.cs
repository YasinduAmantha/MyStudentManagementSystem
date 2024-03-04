using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyStudentManagementSystem
{
    public class ModuleClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public double CreditValue { get; set; }
        public double GradeValue { get; set; }

        public ModuleClass(int id, string name, double credit)
        {
            ID = id;
            Name = name;
            CreditValue = credit;
        }

        public double gradeValue(string Grade)
        {
            if (Grade == "A+")
            {
                GradeValue = 4;
            }
            else if (Grade == "A")
            {
                GradeValue = 4;
            }
            else if (Grade == "A-")
            {
                GradeValue = 3.3;
            }
            else if (Grade == "B")
            {
                GradeValue = 2.7;
            }
            else if (Grade == "C")
            {
                GradeValue = 2;
            }
            else if (Grade == "E")
            {
                GradeValue = 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Grade!!!");
            }

            return GradeValue;
        }
    }
}

