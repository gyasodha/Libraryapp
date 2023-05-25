using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application
{
    public class Books
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("server=IN-8B3K9S3;database=library_management_app;Integrated Security = true");
            con.Open();
            return con;
        }
        public void Add_Book()
        {
            SqlConnection con = GetConnection();
            string query = $"insert into Books(Title,Author,descr_iption,Availabile) values(@Title,@Author,@descr_iption,@Availabile)";
            SqlCommand cmd = new SqlCommand(query, con);
            Console.WriteLine("Enter the Book Title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter the Author Name");
            string author = Console.ReadLine();
            Console.WriteLine("Enter the description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter the Avilabilty of book(y or n)");
            string available = Console.ReadLine();


            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Author", author);
            cmd.Parameters.AddWithValue("@descr_iption", description);
            cmd.Parameters.AddWithValue("@Availabile", available);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Book added successfully");

            con.Close();
        }
        public void Update_Book()
        {
            string connectionString = "Server=IN-8B3K9S3; database=library_management_app; Integrated Security=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Enter the id to update");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the new Book Title:");
                string Title = Console.ReadLine();

                Console.WriteLine("Enter the new Author Name");
                string Author = Console.ReadLine();
                Console.WriteLine("Enter the new description");
                string descr_iption = Console.ReadLine();
                Console.WriteLine("Enter the Avilabilty of book(y or n)");
                string Availabile = Console.ReadLine();



                string updateQuery = $"update Books set Title=@Title,Author=@Author, descr_iption=@descr_iption,Availabile=@Availabile where Book_Id={id}";
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@Author", Author);
                    cmd.Parameters.AddWithValue("@descr_iption", descr_iption);
                    cmd.Parameters.AddWithValue("@Availabile", Availabile);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Book updated Successfully........");
                    }
                    else
                    {
                       Console.WriteLine("Book with that id it is not found......");

                    }
                }
                connection.Close();
            }
        }
        public void Delete_Book()
        {
            string connectionString = "Server=IN-8B3K9S3; database=library_management_app; Integrated Security=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Enter the id to delete");
                int id = Convert.ToInt32(Console.ReadLine());
                string deleteQuery = $"DELETE FROM Books  where Book_Id={id}";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Book_Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Book deleted....");

                    }
                    else
                    {
                        Console.WriteLine("Book with that particular id not found");

                    }
                }
                connection.Close();

            }
        }
        public void View_All_Boooks()
        {
            SqlConnection con = GetConnection();
            string query = $"select * from Books";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                for (int i = 0; i < rd.FieldCount; i++)
                {
                    Console.WriteLine($"{rd[i]}");
                }
                Console.WriteLine();
            }
        }
        public void View_Books_Based_On_Author()
        {
            SqlConnection con = GetConnection();
            Console.WriteLine("Enter the Author name that you want to get");
            string author = Console.ReadLine();
            string query = $"select * from Books where Author = '{author}'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                for (int i = 0; i < rd.FieldCount; i++)
                {
                    Console.WriteLine($"{rd[i]}");
                }
                Console.WriteLine();
            }
        }
        public void Issue_Book()
        {
            string connectionstring = "Server=IN-8B3K9S3; database=library_management_app; Integrated Security=true";
            
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                Console.WriteLine("Enter the particular id:");

                int id = Convert.ToInt16(Console.ReadLine());
                string query = $"select * from Student_details where Student_id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string bookIssued = reader["Book_Issued"].ToString();
                    if (bookIssued.ToLower() == "yes")
                    {
                        Console.WriteLine("Book is already issued....");
                        return;
                    }
                }

                reader.Close();

               
                using (SqlConnection Con = new SqlConnection(connectionstring))
                {
                    Con.Open();
                    Console.WriteLine("Enter the particular id that you want:");
                    int book_id = Convert.ToInt16(Console.ReadLine());
                    string query1 = $"select * from Books where Book_Id = '{book_id}'";
                    SqlCommand Cmd = new SqlCommand(query1, Con);
                    SqlDataReader reader1 = Cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        string Availabile = reader1["Availabile"].ToString();
                        if (Availabile.ToLower() == "no")
                        {
                            Console.WriteLine("Book is already issued to other student present it is not available....");
                            return;
                        }
                    }

                    reader1.Close();


                    
                    using (SqlConnection Con1 = new SqlConnection(connectionstring))
                    {
                        Con1.Open();
                       
                        string query2 = $"update Books set Availabile=@Availabile,Student_id=@Student_id where Book_id = '{book_id}'";
                        SqlCommand cmd1 = new SqlCommand(query2, Con1);
                        cmd1.Parameters.AddWithValue("@Availabile", "NO");
                        cmd1.Parameters.AddWithValue("@Student_id", id);
                        cmd1.ExecuteNonQuery();
                    }
                    
                    using (SqlConnection Con2 = new SqlConnection(connectionstring))
                    {
                        Con2.Open();
                        string query2 = $"update Student_details set Book_Issued=@Book_Issued where Student_id = {id}";
                        SqlCommand cmd2 = new SqlCommand(query2, Con2);
                        cmd2.Parameters.AddWithValue("@Book_Issued", "YES");
                        cmd2.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Book is issued to student");
            }

        }
        public void Return_Book()
        {
            string connectionstring = "Server=IN-8B3K9S3; database=library_management_app; Integrated Security=true";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                Console.WriteLine("Enter the particular student id that you want:");

                int id = Convert.ToInt32(Console.ReadLine());
                string query = $"select * from Student_details where Student_id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string bookIssued = reader["Book_Issued"].ToString();
                    if (bookIssued.ToLower() == "no")
                    {
                        Console.WriteLine("Book is not issued to the student....");
                        return;
                    }
                }

                reader.Close();

                using (SqlConnection Con1 = new SqlConnection(connectionstring))
                {
                    Con1.Open();
                    string query1 = $"update Books set Availabile=@Availabile,Student_id=@Student_id where Student_id = {id}";
                    SqlCommand cmd1 = new SqlCommand(query1, Con1);
                    cmd1.Parameters.AddWithValue("@Availabile", "YES");
                    cmd1.Parameters.AddWithValue("@Student_id", DBNull.Value);

                    cmd1.ExecuteNonQuery();
                }

                using (SqlConnection Con2 = new SqlConnection(connectionstring))
                {
                    Con2.Open();
                    string query2 = $"update Student_details set Book_Issued=@Book_Issued where Student_id = {id}";
                    SqlCommand cmd2 = new SqlCommand(query2, Con2);
                    cmd2.Parameters.AddWithValue("@Book_Issued", "NO");
                    cmd2.ExecuteNonQuery();
                }

                Console.WriteLine("Book is returned");

            }
        }
    }
    
}
