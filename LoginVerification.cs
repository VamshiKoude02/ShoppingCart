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
        private SqlConnection _connection;
        private Users users;

        public LoginVerification(SqlConnection connection)
        {
            _connection = connection;
            users = new Users(_connection);
        }

        public bool UserVerification()
        {
            Console.WriteLine("Welcome to shopping.com");
            Console.WriteLine("Do you have the credentials?(Yes/No)");
            string userInput = Console.ReadLine().ToLower();
            if(userInput == "yes")
            {
                return Users.UserLogin();
            }
            else if(userInput == "no")
            {
                return Users.UserRegister();
            }
            else
            {
                Console.WriteLine("Enter valid Input(Yes/No)....!");
                return false;
            }

        }

    }
}
