using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShopWpf
{
    public class DatabaseManager
    {
        public DatabaseManager(string server,string database, string username,string password) 
        {
            connectionString = $"Server=NOUT;Database=ProductShopWpf; User Id=NAMI; Password=namissms"; 
        }
        public string connectionString { get; private set; }

        public bool ValidateUser(string username, string password)
        {
            using(SqlConnection connection= new SqlConnection(connectionString))
            {
                string query = "SELECT Password FROM Users WHERE Username=@Username";
                using(SqlCommand command= new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    var result= command.ExecuteScalar();
                    if (result != null)
                    {
                        string hashedPassword = result.ToString();
                        return VerifyPassword(password, hashedPassword);
                    }
                }

            }
            return false;
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return password== hashedPassword;
        }
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT*FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal price = 0;
                            if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                            {
                                price = reader.GetDecimal(reader.GetOrdinal("Price"));
                            }

                            Product product = new Product
                            {
                                ProductId = (int)reader["ProductId"],
                                Title = (string)reader["Title"],
                                Discription = (string)reader["Discription"],
                                Price = price

                            };
                            products.Add(product);
                        }
                    }
                }
            }
            return products;

        }
        public void AddProduct(Product product) //Логика добавления товара в бд
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (Title,Discription,Price) VALUES (@Title,@Discription,@Price)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", product.Title);
                    command.Parameters.AddWithValue ("@Discription", product.Discription);
                    command.Parameters.AddWithValue("@Price",product.Price);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }
        public void DeleteProduct(int productId) //Логика удаления товара
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductId=@ProductId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }



        }

    }
    public class Users
    {
        public int Id { get; set; }
        public string Positions { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
