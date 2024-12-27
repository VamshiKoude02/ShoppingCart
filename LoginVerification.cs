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
        public bool loginverification = false;

        private SqlConnection conn;
        public LoginVerification(SqlConnection conn)
        {
            this.conn = conn;
        }

        public void UserVerification()
        {
            Console.WriteLine("Do you have the credentials?(Yes/No)");
            
            while (!loginverification)
            {
                Users user = new Users(conn);
                string userInput = Console.ReadLine().ToLower();
                if (userInput == "yes")
                {
                    
                        user.UserLogin();
                        loginverification = user.loginverification;
                    
                }
                else if (userInput == "no")
                {
                    user.UserRegister();
                    loginverification = user.loginverification;
                }
                else
                {
                    Console.WriteLine("Enter valid Input(Yes/No)....!");
                }
            }
        }
    }
}
