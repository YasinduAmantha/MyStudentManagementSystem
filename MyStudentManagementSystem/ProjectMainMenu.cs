using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyStudentManagementSystem
{
    public class ProjectMainMenu
    {
        public int ColPos { get; set; }
        public int RowPos { get; set; }
        public int SelectedItem { get; set; }
        public List<MainMenuItem> MainMenuItems { get; set; }
        public List<StudentClass> studentClasses = new List<StudentClass>();

        public ProjectMainMenu()
        {
            ColPos = 5;
            RowPos = 5;
            SelectedItem = 0;

            MainMenuItems = new List<MainMenuItem>
            {
                new MainMenuItem("Add User",true),
                new MainMenuItem("Select User",false),
                new MainMenuItem("Delete User",false),
                new MainMenuItem("Display All Users",false),
                new MainMenuItem("Quit",false)
            };

            
        }
        int idx = 0;
        public void DisplayMainMenu()
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
                for (int i = 0; i < MainMenuItems.Count; i++)
                {
                    Console.SetCursorPosition(ColPos, RowPos + i);
                    if (MainMenuItems[i].IsSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(MainMenuItems[i].Title);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(MainMenuItems[i].Title);
                    }
                }

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow)
                {
                    MainMenuItems[SelectedItem].IsSelected = false;
                    SelectedItem = (SelectedItem + 1) % MainMenuItems.Count;
                    MainMenuItems[SelectedItem].IsSelected = true;
                }

                if (key.Key == ConsoleKey.UpArrow)
                {
                    MainMenuItems[SelectedItem].IsSelected = false;
                    SelectedItem = SelectedItem - 1;
                    if (SelectedItem < 0)
                    {
                        SelectedItem = MainMenuItems.Count - 1;
                    }
                    MainMenuItems[SelectedItem].IsSelected = true;
                }

                if (key.Key == ConsoleKey.Enter)
                {



                    Console.Clear();
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(2, 0);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;

                    if (MainMenuItems[SelectedItem].Title == "Quit")
                    {
                        running = false;
                    }
                    else if (MainMenuItems[SelectedItem].Title == "Add User")
                    {
                        add_user();
                    }
                    else if (MainMenuItems[SelectedItem].Title == "Display All Users")
                    {
                        display_users();
                    }
                    else if (MainMenuItems[SelectedItem].Title == "Delete User")
                    {
                        delete_user();
                    }
                    else if (MainMenuItems[SelectedItem].Title == "Select User")
                    {
                        select_user();
                    }
                }
            }
        }

        //Add User Display Menu Function
        void add_user()
        {

            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Student Management System\n*****************************");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            int studIndex = idx + 3800;
            idx++;

            Console.WriteLine("Index Number : " + studIndex);
            Console.Write("First Name: ");
            string first_name = Console.ReadLine();
            Console.Write("Last Name: ");
            string last_name = Console.ReadLine();
            Console.Write("Date of Birth\nPlese enter in MM/dd/yyyy format : ");
            String date_of_birth = Console.ReadLine();
            Console.Write("Permanent Resident Address: ");
            string address = Console.ReadLine();


            StudentClass student = new StudentClass(studIndex, first_name, last_name, date_of_birth, address);
            studentClasses.Add(student);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSuccessfully Add a New Student!!! ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        //Display all users
        void display_users()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All the Students\n*****************************");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Index".PadRight(10) + "First Name".PadRight(15) + "Last Name".PadRight(15) + "Date of Birth".PadRight(20) + "Address".PadRight(10) + "GPA Value".PadRight(20));
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            foreach (var student in studentClasses)
            {
                Console.WriteLine("  " + student.Index.ToString().PadRight(10) + student.First_Name.PadRight(15) + student.Last_Name.PadRight(15) + student.Date_of_Birth.PadRight(20) + student.Address.PadRight(10) + (student.GPACalculation()).ToString("0.000").PadRight(20));
            }

            Console.WriteLine("\n\n\n");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        //Delete student 
        void delete_user()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Student Delete Form\n*****************************");

            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Student Index to delete from Student list : ");
                string s = Console.ReadLine();
                int del_index = int.Parse(s);

                foreach (var student in studentClasses)
                {
                    if (student.Index == del_index)
                    {
                        Console.WriteLine("Index".PadRight(10) + "First Name".PadRight(15) + "Last Name".PadRight(15) + "Date of Birth".PadRight(20) + "Address".PadRight(10) + "GPA Value".PadRight(20));
                        Console.WriteLine("------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("  " + student.Index.ToString().PadRight(10) + student.First_Name.PadRight(15) + student.Last_Name.PadRight(15) + student.Date_of_Birth.PadRight(20) + student.Address.PadRight(10) + (student.GPACalculation()).ToString("0.000").PadRight(20));

                        Console.WriteLine("\nConfirm the deletion (press Y for delete or N for not Delete) : ");

                        var key = Console.ReadKey();

                        if (key.Key == ConsoleKey.Y)
                        {
                            studentClasses.Remove(student);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nSuccessfully Deleted!!!");

                            Console.WriteLine("\n\n");

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }

                        else if (key.Key == ConsoleKey.N)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Deletion is Unsuccessfull!!!");

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nIncorrect key pressed!!!");

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nEntered Index isn't available!!!");

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    break;
                }
            }

            catch(FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEnter Valid Student Index!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

        }

        //Select User Menu Display
        void select_user()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("student and Module Manegement System");
            Console.WriteLine("*************************************");
            int inp_index;
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\nEnter a valid Student Index Number to proceed : ");
                inp_index = Convert.ToInt32(Console.ReadLine());
                bool valid= false;
                foreach(var student in studentClasses)
                {
                    if(inp_index == student.Index)
                    {
                        Console.Clear();
                        Console.WriteLine("Index".PadRight(10) + "First Name".PadRight(15) + "Last Name".PadRight(15) + "Date of Birth".PadRight(20) + "Address".PadRight(10) + "GPA Value".PadRight(20));
                        Console.WriteLine("------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("  " + student.Index.ToString().PadRight(10) + student.First_Name.PadRight(15) + student.Last_Name.PadRight(15) + student.Date_of_Birth.PadRight(20) + student.Address.PadRight(10) + (student.GPACalculation()).ToString("0.000").PadRight(20));
                        
                        Console.WriteLine("\n\n\nPress any key to Proceed ");
                        valid = true;
                        Console.ReadKey();
                        Console.Clear();

                        ProjectSelectUserMenu projectSelectUserMenu = new ProjectSelectUserMenu();
                        projectSelectUserMenu.DisplaySelectUserMenu(student,studentClasses);

                    }
                    else
                    {
                        valid = true;
                        continue;
                    }
                }
                if (valid == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEnter Valid Student Index!!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to back to Main Menu");
                    Console.ReadKey();
                    Console.Clear();
                }
            }



            catch(FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEnter Valid Student Index!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to back to Main Menu");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine("Press any key to back to Main Menu");
            Console.ReadKey();
            Console.Clear();
        }
    }    
}
