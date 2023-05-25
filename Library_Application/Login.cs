using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application
{
    public class Login
    {
        public bool Login_User()
        {
            AnsiConsole.MarkupLine("[green]Login:[/]");
            Console.WriteLine();
            Console.WriteLine("Enter the User name");
            string username = Console.ReadLine();
            Console.WriteLine("Enter the Password");
            string password = Console.ReadLine();

            SqlConnection con = new SqlConnection("server=IN-8B3K9S3;database=library_management_app;Integrated Security = true");
            con.Open();
            string query = "select count(*) from login_user where User_Name = @username and Pass = @password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                AnsiConsole.MarkupLine("[green]Login is done Successfully[/]");
                return true;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Check Your Credentials[/]");
                return false;
            }

            con.Close();
        }
    }
}
