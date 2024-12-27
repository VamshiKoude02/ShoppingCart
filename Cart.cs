using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class CartItems
    {
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public string UserName { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
    }
    class Cart
    {
        private SqlConnection con;
        public Cart(SqlConnection conn)
        {
            con = conn;
        }

        public void CartList()
        {
            List<CartItems> cartlist = new List<CartItems>();
            SqlCommand cmd = new SqlCommand("select * from cart", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CartItems Cartitemsobj = new CartItems();
                    Cartitemsobj.CartID = Convert.ToInt32(dr["CartID"]);
                    Cartitemsobj.ProductID = Convert.ToInt32(dr["ProductID"]);
                    Cartitemsobj.UserName = Convert.ToString(dr["UserName"]);
                    Cartitemsobj.Quantity = Convert.ToInt32(dr["Quantity"]);
                    Cartitemsobj.TotalCost = Convert.ToDouble(dr["TotalCost"]);
                    cartlist.Add(Cartitemsobj);

                }
                dr.Close();
                
            }
            con.Close();
            Console.WriteLine($"CartID - ProductID - UserName - Quantity - TotalCost");
            foreach (var Cartitemsobj in cartlist)
            {
                Console.WriteLine($"{Cartitemsobj.CartID} - {Cartitemsobj.ProductID} - {Cartitemsobj.UserName} - {Cartitemsobj.Quantity}- {Cartitemsobj.TotalCost}");
            }
        }
        public void CartFinalList()
        {
            List<CartItems> cartlist = new List<CartItems>();
            SqlCommand cmd = new SqlCommand("[dbo].[CartFinalList]", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CartItems Cartitemsobj = new CartItems();
                    Cartitemsobj.UserName = Convert.ToString(dr["UserName"]);
                    Cartitemsobj.ProductID = Convert.ToInt32(dr["ProductID"]);
                    Cartitemsobj.Quantity = Convert.ToInt32(dr["Quantity"]);
                    Cartitemsobj.TotalCost = Convert.ToDouble(dr["Cost"]);
                    cartlist.Add(Cartitemsobj);

                }
                dr.Close();
                
            }
            con.Close();
            Console.WriteLine($"UserName - ProductID -  Quantity - Cost");
            foreach (var Cartitemsobj in cartlist)
            {
                Console.WriteLine($"{Cartitemsobj.UserName} - {Cartitemsobj.ProductID} - {Cartitemsobj.Quantity}- {Cartitemsobj.TotalCost}");
            }
        }
    }
}
