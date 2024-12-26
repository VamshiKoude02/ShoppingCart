using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class LoginVerification
    {
        private SqlConnection conn;
        public LoginVerification(SqlConnection conn)
        {
            this.conn = conn;
        }

        public void UserVerification()
        {
            Users user = new Users(conn);
            Console.WriteLine("Welcome to shopping.com");
            Console.WriteLine("Do you have the credentials?(Yes/No)");
            string userInput = Console.ReadLine().ToLower();
            if (userInput == "yes")
            {
                user.UserLogin();
            }
            else if (userInput == "no")
            {
                user.UserRegister();
            }
            else
            {
                Console.WriteLine("Enter valid Input(Yes/No)....!");
            }
        }
    }
}
