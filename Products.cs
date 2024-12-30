using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class Productdetails
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
    class Products
    {
        private SqlConnection con;
        public Products(SqlConnection conn)
        {
            con = conn;
        }

        public void GetProduct()
        {
            List<Productdetails> ProductList = new List<Productdetails>();
            SqlCommand cmd = new SqlCommand("[dbo].[ProductDetails]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Productdetails product = new Productdetails();
                    product.ProductID = Convert.ToInt32(dr["ProductID"]);
                    product.ProductName = Convert.ToString(dr["ProductName"]);
                    product.Price = Convert.ToDouble(dr["Price"]);
                    product.Quantity = Convert.ToInt32(dr["Quantity"]);
                    ProductList.Add(product);

                }
                dr.Close();
                con.Close();
            }
            Console.WriteLine($"ProductID - ProductName - Price - Quantity");
            foreach (var product in ProductList)
            {
                Console.WriteLine($"{product.ProductID} - {product.ProductName} - {product.Price} - {product.Quantity}");
            }
        }
    }
}
