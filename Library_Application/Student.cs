using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application
{
    public class Student
    {
        public static SqlConnection GetConn()
        {
            SqlConnection con = new SqlConnection("server=IN-8B3K9S3;database=library_management_app;Integrated Security = true");
            con.Open();
            return con;
        }
        public void Add_Student()
        {
            SqlConnection con = GetConn();
            string query = $"insert into Student_details values(@Student_iD,@Student_Name,@Book_Issued)";
            SqlCommand cmd = new SqlCommand(query, con);

            Console.WriteLine("Enter the student id");
            int student_id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Student Name");
            string student_name = Console.ReadLine();
            Console.WriteLine("Enter the Status of Book issued(yes or no)");
            string book_issued = Console.ReadLine();

            cmd.Parameters.AddWithValue("@Student_id", student_id);
            cmd.Parameters.AddWithValue("@Student_Name", student_name);
            cmd.Parameters.AddWithValue("@Book_Issued", book_issued);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Student added Successfully");
            con.Close();
        }

        public void Update()
        {
            SqlConnection con = GetConn();
            Console.WriteLine("Enter the id to update");
            int id = Convert.ToInt32(Console.ReadLine());
            string query = $"SELECT COUNT(*) FROM Student_details where Student_id = {id}";
            SqlCommand cmd = new SqlCommand(query, con);
            int rowseffected = (int)cmd.ExecuteScalar();

            if (rowseffected > 0)
            {
                string updatequery = $"update Student_details set Student_Name=@Student_Name,Book_Issued=@Book_Issued where Student_id = {id}";
                SqlCommand command = new SqlCommand(updatequery, con);
                Console.WriteLine("Enter the Student name");
                string Student_Name = Console.ReadLine();
                Console.WriteLine("Enter the updated book issued status");
                string Book_Issued = Console.ReadLine();
                cmd.Parameters.AddWithValue("@Student_Name", Student_Name);
                cmd.Parameters.AddWithValue("@Book_Issued", Book_Issued);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Updated succcessfully");
            }
            else
            {
                Console.WriteLine("ID not found");
            }
            con.Close();


        }
        public void Delete()
        {
            SqlConnection con = GetConn();
            Console.WriteLine("Enter the student id that you want to delete:");
            int id = Convert.ToInt32(Console.ReadLine());
            string query = $"SELECT COUNT(*) FROM Student_details where Student_id = {id}";
            SqlCommand count = new SqlCommand(query, con);
            int rowseffected = (int)count.ExecuteScalar();

            if (rowseffected > 0)
            {
                string deletequery = $"delete from Student_details where Student_id = {id}";
                SqlCommand cmd = new SqlCommand(deletequery, con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Deleted successfully");
            }
            else
            {
                Console.WriteLine($"Record not found with that {id}");
            }

            con.Close();
        }
        public void View_All_Students_havingbooks_rightnow()
        {
            SqlConnection con = GetConn();
            string query = "select * from Student_details";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("Student iD");
            table.AddColumn("Student Name");
            table.AddColumn("Book Taken(YES/NO)");
            table.Title("[underline rgb(131,111,255)]STUDENTS DETAILS[/]");

            while (reader.Read())
            {
                table.AddRow(reader["Student_id"].ToString(), reader["Student_Name"].ToString(), reader["Book_Issued"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }

        public void Search_Student()
        {
            SqlConnection con = GetConn();
           
            int id = AnsiConsole.Ask<int>("[yellow]Enter Student ID you want to view: [/]");
            SqlCommand cmd = new SqlCommand($"select * from  Student_details where Student_id = {id}", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine($" {rd[0]} | {rd[1]} | {rd[2]} ");
            }
            con.Close();
        }

       

        public void Students_Having_Books()
        {
            SqlConnection con = GetConn();
            string query = "select Student_details.Student_id,Student_details.Student_Name,Books.Title,Books.Author,Books.descr_iption from Books join Student_details on Books.Student_id = Student_details.Student_id";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("Student ID");
            table.AddColumn("Student Name");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("description");
            table.Title("[underline rgb(131,111,255)]STUDENTS DETAILS TAKEN BOOKS[/]");
            while (reader.Read())
            {
                table.AddRow(reader["Student_id"].ToString(), reader["Student_Name"].ToString(), reader["Title"].ToString(), reader["Author"].ToString(), reader["descr_iption"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }
    }
}
