using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ShoppingCart
{
    static class Username
    {
        public static string username;
    }
    class Userdetails
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfrimPassword { get; set; }
        public string Mobilenumber { get; set; }
    }

    class Users
    {
        private SqlConnection con;
        public Users(SqlConnection conn)
        {
            con = conn;
        }
        Userdetails user = new Userdetails();

        public bool loginverification = false;
        public void UserLogin()
        {
                
            while (!loginverification)
            {
                Console.WriteLine("Enter Username:");
                user.Username = Console.ReadLine();
                Console.WriteLine("Enter Password");
                user.Password = Console.ReadLine();
                SqlCommand cmd = new SqlCommand("[dbo].[UserLogin]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);

                try
                {
                    ShoppingProcess shopppingprocess = new ShoppingProcess(con);
                    con.Open();
                    int result = (int)cmd.ExecuteScalar();
                    con.Close();
                    if (result == 1)
                    {
                        Console.WriteLine("User login success..");
                        loginverification = true;
                        Username.username = user.Username;
                        shopppingprocess.ShoppingOptions();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Credentials.Please try again..!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            
        }

        public void UserRegister()
        {    
            Console.WriteLine("Enter Name:");
            user.Name = Console.ReadLine();
            Console.WriteLine("Enter Username:");
            user.Username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            user.Password = Console.ReadLine();
            ValidPassword:
            Console.WriteLine("Enter Confrim Password");
            user.ConfrimPassword = Console.ReadLine();
            if(user.ConfrimPassword != user.Password)
            {
                Console.WriteLine("Password didn't match.Try again..!");
                goto ValidPassword;
            }
            CorrectNumber:
            Console.WriteLine("Enter Mobile Number");
            user.Mobilenumber = Console.ReadLine();
            if( user.Mobilenumber.Length!=10)
            {
                Console.WriteLine("Enter correct Mobile Number");
                goto CorrectNumber;
            }
            SqlCommand cmd = new SqlCommand("[dbo].[UserRegister]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@name",user.Name);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@mobilenumber", user.Mobilenumber);
            try
            {
                ShoppingProcess shopppingprocess = new ShoppingProcess(con);
                con.Open();
                int result = (int)cmd.ExecuteScalar();
                con.Close();
                if (result == 0)
                {
                    Console.WriteLine("User Already Exists..!");
                    Console.WriteLine("Do you want to Login.?(yes/no)");
                    string input = Console.ReadLine().ToLower();
                    if (input == "yes")
                    {
                        UserLogin();
                    }
                    else
                    {
                        Console.WriteLine("Unable to Login");
                    }
                }
                else
                {
                    Console.WriteLine("User created successfully..");
                    string finalusername = user.Username;
                    loginverification = true;
                    Username.username = user.Username;
                    shopppingprocess.ShoppingOptions();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
