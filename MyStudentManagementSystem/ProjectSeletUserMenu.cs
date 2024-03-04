using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyStudentManagementSystem
{
    public class ProjectSelectUserMenu
    {
        public int ColPos { get; set; }
        public int RowPos { get; set; }
        public int SelectedItem { get; set; }
        public List<MainMenuItem> SelectUserItems { get; set; }

        public ProjectSelectUserMenu()
        {
            ColPos = 5;
            RowPos = 5;
            SelectedItem = 0;

            SelectUserItems = new List<MainMenuItem>
            {
                new MainMenuItem("Modify User",true),
                new MainMenuItem("Add Modules",false),
                new MainMenuItem("Remove Modules",false),
                new MainMenuItem("Grade Calculation",false),
                new MainMenuItem("Delete User",false),
                new MainMenuItem("Back",false)
            };
        }

        //Check Module is already added
        public bool check_add(List<ModuleClass> modules, int id)
        {
            bool isAdd = false;
            foreach (ModuleClass module in modules)
            {
                if (module.ID == id)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Allready Inserted Module!!!");
                    isAdd = true;
                }
            }
            return isAdd;
        }
        public void DisplaySelectUserMenu(StudentClass student,List<StudentClass> students)
        {
            
            SelectedItem = 0;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();                                    //Clear the terminal
            Console.CursorVisible = false;                      // setting cursor visibility false
            bool running = true;
            while (running)
            {
                Console.SetCursorPosition(ColPos, RowPos);
                for (int i = 0; i < SelectUserItems.Count; i++)
                {
                    Console.SetCursorPosition(ColPos, RowPos + i);
                    if (SelectUserItems[i].IsSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(SelectUserItems[i].Title);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(SelectUserItems[i].Title);
                    }
                }

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow)
                {
                    SelectUserItems[SelectedItem].IsSelected = false;
                    SelectedItem = (SelectedItem + 1) % SelectUserItems.Count;
                    SelectUserItems[SelectedItem].IsSelected = true;
                }

                if (key.Key == ConsoleKey.UpArrow)
                {
                    SelectUserItems[SelectedItem].IsSelected = false;
                    SelectedItem = SelectedItem - 1;
                    if (SelectedItem < 0)
                    {
                        SelectedItem = SelectUserItems.Count - 1;
                    }
                    SelectUserItems[SelectedItem].IsSelected = true;
                }

                if(key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.SetCursorPosition(2, 0);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;

                    if (SelectUserItems[SelectedItem].Title == "Back")
                    {
                        Console.Clear();
                        running = false;
                    }

                    else if(SelectUserItems[SelectedItem].Title == "Modify User")
                    {
                        modify_user(student);
                    }
                    else if (SelectUserItems[SelectedItem].Title == "Grade Calculation")
                    {
                        grade_calculation(student);
                    }

                    else if (SelectUserItems[SelectedItem].Title == "Add Modules")
                    {
                        add_modules(student);
                    }
                    else if (SelectUserItems[SelectedItem].Title == "Delete User")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Delete Student Form");
                        Console.WriteLine("********************");

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Do you need to delete the current student(Press Y to YES, N to NO)");

                        string user_input = Console.ReadLine();
                        if (user_input == "y" || user_input == "Y")
                        {
                            students.Remove(student);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nStudent Delete Successfully!!!");

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to Continue");
                            Console.ReadKey();
                            Console.Clear();
                            running = false;
                        }

                        else if (user_input == "n" || user_input == "N")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nUnsuccessfull Deletion!!!");

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to Continue");
                            Console.ReadKey();
                            Console.Clear();
                            running = true;
                            return;
                        }
                    }
                    else if (SelectUserItems[SelectedItem].Title == "Remove Modules")
                    {
                        remove_module(student);
                    }
                }
            }
        }

        //Modify Student Function
        void modify_user(StudentClass student)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Modify Student Details.");
            Console.WriteLine("************************");
            Console.WriteLine("\n\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1.) Modify First Name");
            Console.WriteLine("2.) Modify Last Name");
            Console.WriteLine("3.) Modify Date Of Birth (MM/DD/YYYY)");
            Console.WriteLine("4.) Modify Address");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter Relavent number to Proceed : ");
            int N;

            try
            {
                N = Convert.ToInt32(Console.ReadLine());
                if (N == 1)
                {
                    Console.WriteLine("Current First Name : " + student.First_Name);
                    Console.WriteLine("Enter New First Name : ");
                    string new_fName = Console.ReadLine();
                    Console.WriteLine("Are you Sure to update student details (Press Y for Yes, N for NO )");
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nSuccessfully Updated!!!");
                        student.First_Name = new_fName;
                    }

                    else if (key.Key == ConsoleKey.N)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNot Updated Succcessfully!!!");
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\ninvalid key !!!");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\nPress any key to Go Back");
                    Console.ReadKey();
                    Console.Clear();
                    
                }

                else if (N == 2)
                {
                    Console.WriteLine("Current Last Name : " + student.Last_Name);
                    Console.WriteLine("Enter New Last Name : ");
                    string new_lName = Console.ReadLine();
                    Console.WriteLine("Are you Sure to update student details (Press Y for Yes, N for NO )");
                    
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nSuccessfully Updated!!!");
                        student.Last_Name = new_lName;
                    }

                    else if (key.Key == ConsoleKey.N)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNot Updated Succcessfully!!!");
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\ninvalid key !!!");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\nPress any key to Go Back");
                    Console.ReadKey();
                    Console.Clear();
                }

                else if (N == 3)
                {
                    Console.WriteLine("Current Date of Birth : " + student.Date_of_Birth);
                    Console.WriteLine("Enter New Date of Birth : ");
                    string new_dob = Console.ReadLine();
                    Console.WriteLine("Are you Sure to update student details (Press Y for Yes, N for NO )");
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nSuccessfully Updated!!!");
                        student.Date_of_Birth = new_dob;
                    }

                    else if (key.Key == ConsoleKey.N)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNot Updated Succcessfully!!!");
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\ninvalid key !!!");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\nPress any key to Go Back");
                    Console.ReadKey();
                    Console.Clear();
                }

                else if (N == 4)
                {
                    Console.WriteLine("Current Address : " + student.Address);
                    Console.WriteLine("Enter New Address : ");
                    string new_address = Console.ReadLine();
                    Console.WriteLine("Are you Sure to update student details (Press Y for Yes, N for NO )");
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nSuccessfully Updated!!!");
                        student.Address = new_address;
                    }

                    else if (key.Key == ConsoleKey.N)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNot Updated Succcessfully!!!");
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\ninvalid key !!!");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\nPress any key to Go Back");
                    Console.ReadKey();
                    Console.Clear();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\ninvalid key !!!");
                    

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\nPress any key to Go Back");
                    Console.ReadKey();
                    Console.Clear();
                }

            }
            catch(FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("invalid key !!!");


                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to Go Back");
                Console.ReadKey();
                Console.Clear();
                return;
            }            
        }

        //Delete User Function

        //Back to Menu
        public void BacktoMenu()
        {
            bool isback = true;
            while (isback)
            {
                Console.WriteLine("\n\n\n\n\n\t\t\t\t\t-Press any key to Return to Main Menu-\n");
                var key1 = Console.ReadKey();
                Console.Clear();
                break;
            }
        }


        //Remove Module Function
        public void remove_module(StudentClass student)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Remove Module Form");
            Console.WriteLine("*******************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.CursorVisible = true;
            Console.WriteLine();

            Console.WriteLine("__________________________________________________________________________");
            Console.WriteLine("\tModule ID\t|\t\tModule Name");
            Console.WriteLine("__________________________________________________________________________");

            foreach (ModuleClass module in student.Modules)
            {
                Console.WriteLine("\t" + module.ID + "\t\t|\t" + module.Name);
            }
            Console.WriteLine();

            bool Remove = false;
            while (!Remove)
            {
                try
                {
                    Console.WriteLine("Enter the Module ID to Delete the Module: ");
                    int module_Id = Convert.ToInt32(Console.ReadLine());
                    bool in_module = false;
                    foreach (ModuleClass module in student.Modules)
                    {
                        if (module_Id == module.ID)
                        {
                            student.Modules.Remove(module);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Successfully Deleted Module!!!");
                            Console.ForegroundColor = ConsoleColor.White;
                            in_module = true;
                            break;
                        }
                    }
                    if (!in_module)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Module ID.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input. Plz Check Again.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Do you want to Delete more Modules (press Y for YES or N for NO): ");
                bool isValid = false;
                while (!isValid)
                {
                    string input_key = Console.ReadLine();
                    if (input_key == "y" || input_key == "Y")
                    {
                        isValid = true;
                        Remove = false;
                        break;
                    }
                    else if (input_key == "n" || input_key == "N")
                    {
                        isValid = true;
                        Remove = true;
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter Valid Input!!!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to Go Back");
            Console.ReadKey();
            Console.Clear();
        }
        
        // Add Grade Function

        void grade_calculation(StudentClass student)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Grade Calculation");
            Console.WriteLine("*****************");
            Console.WriteLine();

            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("________________________________________________________________________");
            Console.WriteLine("|\tModule ID\t|\t\tModule Name\t\t\t|");
            Console.WriteLine("________________________________________________________________________");

            foreach (ModuleClass module in student.Modules)
            {
                Console.WriteLine("\t" + module.ID + "\t\t|\t" + module.Name);
            }

            Console.WriteLine();
            try
            {
                foreach (ModuleClass module in student.Modules)
                {
                    Console.Write("Enter Grade for " + module.Name + ": ");
                    string grade = Console.ReadLine();
                    module.Grade = grade;
                    module.gradeValue(grade);
                }
                Console.WriteLine("Press any key to Go Back");
                Console.ReadKey();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.Clear();
                //return;
                //Console.Clear();
                
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Input");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to Go Back");
                Console.ReadKey();
            }
        }

        
        //Add Module function
        void add_modules(StudentClass student)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Module Adding System");
            Console.WriteLine("**********************");

            Console.WriteLine("\n\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Module ID\t|\tModule Name\t\t\t|\tCredits");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────────");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("3301\t\t\tAnalog Electronics\t\t\t3");
            Console.WriteLine("3302\t\t\tData Structures and Algorithms\t\t3");
            Console.WriteLine("3151\t\t\tProgramming Project\t\t\t1");
            Console.WriteLine("3305\t\t\tSignals and Systems\t\t\t3");
            Console.WriteLine("3201\t\t\tBasic Economics\t\t\t\t3");
            Console.WriteLine("3250\t\t\tGUI Programming\t\t\t\t2");

            Console.WriteLine("\n\n");

            bool add_module = false;

            while(!add_module)
            {
                Console.CursorVisible = true;   
                Console.WriteLine("Enter Module Index : ");
                string mod_index = Console.ReadLine();

                switch (mod_index)
                {
                    case "3301":
                        if (!check_add(student.Modules, Convert.ToInt32(mod_index)))
                        {
                            ModuleClass module01 = new ModuleClass(3301, "Analog Electronics", 3);
                            student.Modules.Add(module01);

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Module added Successfully!!!");
                        }
                        break;

                    case "3302":
                        if (!check_add(student.Modules, Convert.ToInt32(mod_index)))
                        {
                            ModuleClass module02 = new ModuleClass(3302, "Data Structures and Algorithms", 3);
                            student.Modules.Add(module02);

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Module added Successfully!!!");
                        }
                        break;

                    case "3151":
                        if (!check_add(student.Modules, Convert.ToInt32(mod_index)))
                        {
                            ModuleClass module03 = new ModuleClass(3151, "Programming Project", 3);
                            student.Modules.Add(module03);

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Module added Successfully!!!");
                        }
                        break;

                    case "3305":
                        if (!check_add(student.Modules, Convert.ToInt32(mod_index)))
                        {
                            ModuleClass module04 = new ModuleClass(3305, "Signals and Systems", 3);
                            student.Modules.Add(module04);

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Module added Successfully!!!");
                        }
                        break;

                    case "3201":
                        if (!check_add(student.Modules, Convert.ToInt32(mod_index)))
                        {
                            ModuleClass module05 = new ModuleClass(3201, "Basic Economics", 3);
                            student.Modules.Add(module05);

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Module added Successfully!!!");
                        }
                        break;
                    case "3250":
                        if (!check_add(student.Modules, Convert.ToInt32(mod_index)))
                        {
                            ModuleClass module06 = new ModuleClass(3250, "GUI Programming", 3);
                            student.Modules.Add(module06);

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Module added Successfully!!!");
                        }
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid ID!!!");
                        break;
                }

                Console.WriteLine("\n");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Do You want To Add More Modules(press Y for YES, N for NO): ");
                bool isValid = false;
                while (!isValid)
                {
                    string user_input = Console.ReadLine();
                    if (user_input == "y" || user_input == "Y")
                    {
                        isValid = true;
                        add_module = false;
                        break;
                    }
                    else if (user_input == "n" || user_input == "N")
                    {
                        isValid = true;
                        add_module = true;
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input!!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Enter Valid Input");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to Go Back");
            Console.ReadKey();
            Console.Clear();
            return;
        }
    }
}
