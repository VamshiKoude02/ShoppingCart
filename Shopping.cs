using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class Shopping
    {
        static void Main(string[] args)
        {
            string constr = ConfigurationManager.ConnectionStrings["shoppingCart"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            Users users = new Users(conn);
            LoginVerification login = new LoginVerification(conn);
            login.UserVerification();
        }
    }
}
