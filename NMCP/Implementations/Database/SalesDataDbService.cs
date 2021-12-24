using NMCP.Abstractions.Models;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
{
    public class SalesDataDbService
    {
        private readonly string _connectionString;
        public SalesDataDbService()
        {
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(ISalesData salesData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO SalesData" +
                    "(SaleId, DistributorId,  SaleDate, ProductId, UnitsSold, UnitsTotalPrice) VALUES" +
                    $"({salesData.SaleId}, {salesData.DistributorId},{salesData.SaleDate},{salesData.ProductId},{salesData.UnitsSold},{salesData.UnitsTotalPrice} ");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateItemInCollection(ISalesData salesData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE SalesData SET" +
                    $"UnitsSold = {salesData.UnitsSold}, UnitsTotalPrice = {salesData.UnitsTotalPrice} WHERE SaleId = {salesData.SaleId}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void DeleteItemInCollection(ISalesData salesData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM SalesData WHERE SaleId = {salesData.SaleId}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public ISalesData GetSaleById(int SaleId)
        {
            List<ISalesData> salesData = new List<ISalesData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT FROM SalesData WHERE SaleId = {SaleId}");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesData.Add(new SalesDataModel()
                        {
                            SaleId = reader["SaleId"].ToString(),
                            SaleDate = reader["SaleId"].ToString(),
                            UnitsTotalPrice = Convert.ToInt32(reader["UnitsTotalPrice"]),
                            UnitsSold = Convert.ToInt32(reader["UnitsSold"]),
                            DistributorId = Convert.ToInt32(reader["DistributorId"]),
                            ProductId = Convert.ToInt32(reader["ProductId"])
                        });
                    }
                }
                conn.Close();
            }
            return salesData.First();
        }
        public List<ISalesData> GetSalesByUserId(int DistributorId)
        {
            List<ISalesData> salesData = new List<ISalesData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM SalesData WHERE DistributorId = {DistributorId}");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesData.Add(new SalesDataModel()
                        {
                            SaleId = reader["SaleId"].ToString(),
                            SaleDate = reader["SaleId"].ToString(),
                            UnitsTotalPrice = Convert.ToInt32(reader["UnitsTotalPrice"]),
                            UnitsSold = Convert.ToInt32(reader["UnitsSold"]),
                            DistributorId = Convert.ToInt32(reader["DistributorId"]),
                            ProductId = Convert.ToInt32(reader["ProductId"])
                        });
                    }
                }
                conn.Close();
            }
            return salesData;
        }
        public List<ISalesData> GetSales()
        {
            List<ISalesData> salesData = new List<ISalesData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM SalesData");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesData.Add(new SalesDataModel()
                        {
                            SaleId = reader["SaleId"].ToString(),
                            SaleDate = reader["SaleId"].ToString(),
                            UnitsTotalPrice = Convert.ToInt32(reader["UnitsTotalPrice"]),
                            UnitsSold = Convert.ToInt32(reader["UnitsSold"]),
                            DistributorId = Convert.ToInt32(reader["DistributorId"]),
                            ProductId = Convert.ToInt32(reader["ProductId"])
                        });
                    }
                }
                conn.Close();
            }
            return salesData;
        }
    }
}
