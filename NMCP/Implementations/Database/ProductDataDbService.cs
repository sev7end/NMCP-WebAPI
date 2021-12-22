using NMCP.Abstractions.Models;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
{
    public class ProductDataDbService
    {
        private readonly string _connectionString;
        public ProductDataDbService()
        {
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(IProductData productData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO ProductData" +
                    "(ProductName, UnitPrice) VALUES" +
                    $"({productData.ProductName}, {productData.UnitPrice}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateItemInCollection(IProductData productData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE ProductData SET" +
                    $"ProductName = {productData.ProductName}, UnitPrice = {productData.UnitPrice} WHERE Id = {productData.Id}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void DeleteItemInCollection(IProductData productData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM ProductData WHERE Id = {productData.Id}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public IProductData GetProductById(int Id)
        {
            List<IProductData> productData = new List<IProductData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT FROM ProductData WHERE Id = {Id}");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productData.Add(new ProductDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ProductName = reader["country"].ToString(),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                        });
                    }
                }
                conn.Close();
            }
            return productData.First();
        }
        public List<IProductData> GetProducts()
        {
            List<IProductData> productData = new List<IProductData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM ProductData");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productData.Add(new ProductDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ProductName = reader["country"].ToString(),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                        });
                    }
                }
                conn.Close();
            }
            return productData;
        }
    }
}
