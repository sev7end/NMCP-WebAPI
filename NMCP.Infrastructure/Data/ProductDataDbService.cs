﻿using NMCP.Core.Models;
using NMCP.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Data
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
                    "(@ProductName, @UnitPrice)");
                cmd.Parameters.Add(new SqlParameter("ProductName", productData.ProductName));
                cmd.Parameters.Add(new SqlParameter("UnitPrice", productData.UnitPrice));
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
                    $"ProductName = @ProductPrice, UnitPrice = @UnitPrice WHERE Id = @Id");
                cmd.Parameters.Add(new SqlParameter("ProductName", productData.ProductName));
                cmd.Parameters.Add(new SqlParameter("UnitPrice", productData.UnitPrice));
                cmd.Parameters.Add(new SqlParameter("Id", productData.Id));
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
                SqlCommand cmd = new SqlCommand("DELETE FROM ProductData WHERE Id = @Id");
                cmd.Parameters.Add(new SqlParameter("Id", productData.Id));
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM ProductData WHERE Id = @Id");
                cmd.Parameters.Add(new SqlParameter("Id",Id));
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productData.Add(new ProductDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ProductName = reader["ProductName"].ToString(),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"].ToString())
                        });
                    }
                }
                conn.Close();
            }
            return productData.Count != 0 ? productData.First() : null;
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
                            ProductName = reader["ProductName"].ToString(),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"].ToString())
                        });
                    }
                }
                conn.Close();
            }
            return productData;
        }
    }
}
