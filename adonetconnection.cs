using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApplication28
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Data link from Northwind properties
                using (SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    //using (SqlCommand cmd = new SqlCommand())
                    //{
                    //    cmd.CommandText = "SELECT * FROM CUSTOMERS";
                    //    cmd.Connection = conn;
                    //    using (SqlDataReader reader = cmd.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            string contactName = (string)reader["ContactName"];
                    //            string City = (string)reader["City"];
                    //            Console.Write(String.Format("{0, -20}, {1, -10}\n", contactName, City));
                    //        }



                    //    }
                    //}
                    //using (SqlCommand cmd = new SqlCommand())
                    //{
                    //    cmd.CommandText = "Ten Most Expensive Products";
                    //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //    cmd.Connection = conn;
                    //    using (SqlDataReader reader = cmd.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            string name = (string)reader["TenMostExpensiveProducts"];
                    //            Console.WriteLine(name);
                    //        }
                    //    }
                    //}



                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SalesByCategory";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("CategoryName", "Seafood"));
                        cmd.Connection = conn;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                //string name = (string)reader["ProductName"];
                                string name = (string)reader[0];
                                decimal price = (decimal)reader[1];
                                Console.WriteLine(String.Format("{0, -25}, {1, -10}\n", name, price));
                                }
                                //InvalidCastException e, Tha vgalei sfalma ean o tipos tis metavlitis den einai swstos, px double price adi gia decimal price
                                catch (InvalidCastException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                //IndexOutOfRangeException e1, O pinakas sou vgazei sfalma ean pame na zitisoue stoixeia apo kathgoriapou den iparxei, px decimal price = (decimal)reader[1]; den iparxei 3i stili
                                catch (IndexOutOfRangeException e1)
                                {
                                    Console.WriteLine(e1.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Console.WriteLine("Error:" + ex.Errors[i].ToString());
                }
            }
            //Kai na vgei exception kai na mi vgei, to finally diasfalizei oti oti einai mesa se afto tha ektelestei
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
