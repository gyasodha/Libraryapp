namespace Library_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------Welcome to Library Management App------------------------------");

            Login login = new Login();
            Student students = new Student();
            Books books = new Books();
            bool Is_Logged_In = login.Login_User();
            while (!Is_Logged_In)
            {
                Is_Logged_In = login.Login_User();
            }
            Console.WriteLine();
            while (Is_Logged_In)
            {
                Console.WriteLine();
                Console.WriteLine("Enter your choice:");
                Console.WriteLine("1. Add the student");
                Console.WriteLine("2. Update the student");
                Console.WriteLine("3. Delete the student");
                Console.WriteLine("4. Search the student");
                Console.WriteLine("5. Students Having Books");
                Console.WriteLine("6. View All Students having books right now");
                Console.WriteLine("7.Add the book");
                Console.WriteLine("8. Update the book");
                Console.WriteLine("9. Delete the book");
                Console.WriteLine("10. View All Books");
                Console.WriteLine("11. View Books By Author");
                Console.WriteLine("12. Issue Book");
                Console.WriteLine("13. Return Book");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            students.Add_Student();
                            break;
                        case 2:
                            students.Update();
                            break;

                        case 3:
                            students.Delete();
                            break;

                        case 4:
                            students.Search_Student();
                            break;
                        case 5:
                            students.Students_Having_Books();
                            break;

                        case 6:

                            students.View_All_Students_havingbooks_rightnow();
                            break;


                         case 7:
                             books.Add_Book();
                             break;

                         case 8:
                            books.Update_Book();
                             break;
                        case 9:
                            books.Delete_Book();
                             break;
                     case 10:
                             books.View_All_Boooks();
                             break;
                         case 11:
                             books.View_Books_Based_On_Author();
                             break;
                         case 12:
                             books.Issue_Book();
                             break;
                        case 13:
                             books.Return_Book();
                             break;   

                        default:
                            Console.WriteLine("Wrong Choice Entered!");
                            break;
                    }
                }

            }
        }

    }
}
