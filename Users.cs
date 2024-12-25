using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class Users
    {
        private string _name;
        private string _username;
        private string _password;
        private string _mobilenumber;
        private SqlConnection _connection;

        public Users(SqlConnection connection)
        {
            _connection = connection;

        }
        public string Name {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                else
                {
                    Console.WriteLine("Name Can't be Blank");
                }
            }
            get
            {
                return _name;
            }
        }
        public string Username {
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _username = value;
                }
                else
                {
                    Console.WriteLine("Username Can't be Blank");
                }
            }
            get
            {
                return _username;
            }
        }
        public string Password {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _password = value;
                }
                else
                {
                    Console.WriteLine("Password Can't be Blank");
                }
            }
            get
            {
                return _password;
            }

        }
        public string MobileNumber {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _mobilenumber = value;
                }
                else
                {
                    Console.WriteLine("Mobile Number Can't be Blank");
                }
            }
            get
            {
                return _mobilenumber;
            }
        }

        public bool UserLogin(string username, string password)
        {
            return true;
        }

        public bool UserRegister()
        {
            return false;
        }
    }
}
