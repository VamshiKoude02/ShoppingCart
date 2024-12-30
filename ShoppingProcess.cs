using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ShoppingCart
{
    class ShoppingProcess
    {
        private SqlConnection con;
        public ShoppingProcess(SqlConnection conn)
        {
            con = conn;
        }
        public void ShoppingOptions()
        {
            Addtocart:
            Console.WriteLine("What you want to do now?");
            Console.WriteLine("1.Product List");
            Console.WriteLine("2.Add to cart");
            Console.WriteLine("3.View Cart Items");
            Console.WriteLine("4.Place an Order");
            Console.WriteLine("5.Account Details");
            Console.WriteLine("6.Exit");
            int input = Convert.ToInt32(Console.ReadLine());
            try
            {
                Users user = new Users(con);
                Products product = new Products(con);
                Cart cart = new Cart(con);
                if (input == 1)
                {
                    product.GetProduct();
                    goto Addtocart;
                }
                else if (input == 2)
                {
                    product.GetProduct();
                    Console.WriteLine("Choose Product Number to add to cart.");
                    int ProductId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Choose Quantity.");
                    int Quantity = Convert.ToInt32(Console.ReadLine());
                    SqlCommand cmd = new SqlCommand("[dbo].[AddItemsToCart]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", ProductId);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@username", Username.username);
                    con.Open();
                    string result = (string)cmd.ExecuteScalar();
                    con.Close();
                    Console.WriteLine(result);
                    goto Addtocart;
                }
                else if (input == 3)
                {
                    cart.CartFinalList();
                    goto Addtocart;
                }

                else if (input == 4)
                {
                    cart.CartFinalList();
                    iscartok:
                    Console.WriteLine("Are you okay with cart Items..?");
                    string cartok = Console.ReadLine();
                    if (cartok == "yes") {
                        SqlCommand cmd = new SqlCommand("[dbo].[IsCartOk]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cartok", cartok);
                        cmd.Parameters.AddWithValue("@username", Username.username);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int totalqunatity = Convert.ToInt32(dr["TotalQuantity"]);
                                double totalcost = Convert.ToDouble(dr["TotalCost"]);
                                Console.WriteLine(totalqunatity + " of Items of Price " + totalcost + " Added to your cart successfully..");
                            }
                            
                        }
                        con.Close();
                        Console.WriteLine("Do you want to proceed for payment?");
                        string payment = Console.ReadLine().ToLower();
                        if(payment == "yes")
                        {
                            Console.WriteLine("Payment Module is under development");
                            Console.WriteLine("Payment sucessful");
                            SqlCommand cmdd = new SqlCommand("[dbo].[StoreOrdersandEmptyCart]", con);
                            con.Open();
                            int i = (int)cmdd.ExecuteScalar();
                            con.Close();
                            if (i > 0)
                            {
                                Console.WriteLine("Cart cleared sucessfully..!");
                            }
                            else
                            {
                                Console.WriteLine("Cart is Empty..!");
                                goto iscartok;
                            }
                        }
                        else
                        {
                            goto Addtocart;
                        }
                    }
                    else if(cartok == "no")
                    {
                        CartItems cartItems = new CartItems();
                        Cart cartlist = new Cart(con);
                        Console.WriteLine("Do you want to remove any items?");
                        string removeitems = Console.ReadLine().ToLower();
                        if(removeitems == "yes")
                        {
                            cartlist.CartList();
                            Console.WriteLine("Which item Do you want to remove? Please Enter CartID of it.");
                            int cartno = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Quantity");
                            int Quantity = Convert.ToInt32(Console.ReadLine());
                            SqlCommand cmd = new SqlCommand("[dbo].[IsCartNotOK]",con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.AddWithValue("@cartID", cartno);
                            cmd.Parameters.AddWithValue("@Quantity",Quantity);
                            string output = (string)cmd.ExecuteScalar();
                            con.Close();
                            Console.WriteLine(output);
                            goto iscartok;
                        }
                        else
                        {
                            goto Addtocart;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter Correct Input..");
                        goto iscartok;
                    }

                }
                else if(input == 5)
                {
                    Console.WriteLine("User name :" + Username.username);
                    SqlCommand cmd = new SqlCommand("[dbo].[mobilenumber]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@username", Username.username);
                    con.Open();
                    string mobilenumber = (string)cmd.ExecuteScalar();
                    con.Close() ;
                    Console.WriteLine("Mobile Number :" + mobilenumber);
                }
                else if(input == 6)
                {
                    Console.WriteLine("Double Press Any Key to Exit..");
                    return;
                }
                else
                {
                    Console.WriteLine("Enter Correct Input...!");
                    goto Addtocart;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
