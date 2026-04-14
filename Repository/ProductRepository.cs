// Pasta para acessar o Banco de Dados
// Recebe os Dados > Executa o Banco de dados > Retorna Resultados
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using InventoryApp.Models;
using System.Security.Claims;

namespace InventoryApp.Data
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Products 
                                (Name, Brand, Quantity, LastPurchase, Origin) 
                                VALUES (@Name, @Brand, @Quantity, @LastPurchase, @Origin)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Brand", product.Brand);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@LastPurchase", product.LastPurchase);
                cmd.Parameters.AddWithValue("@Origin", product.Origin);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Brand = reader["Brand"].ToString(),
                        Quantity = (int)reader["Quantity"],
                        LastPurchase = (DateTime)reader["LastPurchase"],
                        Origin = reader["Origin"].ToString()
                    };

                    products.Add(product);
                }

                reader.Close();
            }

            return products;
        }

        public Product GetProductByNameAndBrandAndOrigin(string name, string brand, string origin)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT TOP 1 * FROM Products WHERE Name = @Name AND Brand = @Brand AND Origin = @Origin";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Brand", brand);
                cmd.Parameters.AddWithValue("@Origin", origin);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Product
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Brand = reader["Brand"].ToString(),
                        Quantity = (int)reader["Quantity"],
                        LastPurchase = (DateTime)reader["LastPurchase"],
                        Origin = reader["Origin"].ToString()
                    };
                }
                return null;
            }
        }

        public void UpdateProduct(int Id, int newQuantity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"UPDATE Products SET Quantity = @Quantity , LastPurchase = @LastPurchase WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                cmd.Parameters.AddWithValue("@LastPurchase", DateTime.Now);
                cmd.Parameters.AddWithValue("@Id",Id);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> SearchByName(string name)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Products WHERE Name LIKE @Name";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name",$"%{name}%");
                
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id =  (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Brand = reader["Brand"].ToString(),
                        Quantity = (int)reader["Quantity"],
                        LastPurchase = (DateTime)reader["LastPurchase"],
                        Origin = reader["Origin"].ToString()
                    });
                }
            }
            return products;
        }

        public void UpdateFullProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();   

                string query = @"UPDATE Products 
                                SET Name = @Name,
                                Brand = @Brand,
                                Quantity = @Quantity,
                                Origin = @Origin,
                                LastPurchase = @LastPurchase
                                WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query,conn);

                cmd.Parameters.AddWithValue("@Name",product.Name);
                cmd.Parameters.AddWithValue("@Brand",product.Brand);
                cmd.Parameters.AddWithValue("@Quantity",product.Quantity);
                cmd.Parameters.AddWithValue("@Origin",product.Origin);
                cmd.Parameters.AddWithValue("@LastPurchase",product.LastPurchase);
                cmd.Parameters.AddWithValue("@Id",product.Id);

                cmd.ExecuteNonQuery();
            }
 
        }

        public Product GetProductById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Products WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Id",id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Product
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Brand = reader["Brand"].ToString(),
                        Quantity = (int)reader["Quantity"],
                        LastPurchase = (DateTime)reader["LastPurchase"],
                        Origin = reader["Origin"].ToString()
                    };
                }
                return null;
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Products WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Id",id);

                cmd.ExecuteNonQuery();
            }
            

        }
    }
}