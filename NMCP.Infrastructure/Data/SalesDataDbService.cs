using NMCP.Core.Models;
using NMCP.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Data
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
                    "(@SaleId, @DistributorId,@SaleDate,@ProductId,@UnitsSold,@TotalPrice)");
                cmd.Parameters.Add(new SqlParameter("SaleId", salesData.SaleId));
                cmd.Parameters.Add(new SqlParameter("DistributorId", salesData.DistributorId));
                cmd.Parameters.Add(new SqlParameter("SaleDate", salesData.SaleDate));
                cmd.Parameters.Add(new SqlParameter("ProductId", salesData.ProductId));
                cmd.Parameters.Add(new SqlParameter("UnitsSold", salesData.UnitsSold));
                cmd.Parameters.Add(new SqlParameter("TotalPrice", salesData.UnitsTotalPrice));
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
                    "UnitsSold =@UnitsSold, UnitsTotalPrice = @TotalPrice WHERE SaleId = @SaleId");
                cmd.Parameters.Add(new SqlParameter("SaleId", salesData.SaleId));
                cmd.Parameters.Add(new SqlParameter("DistributorId", salesData.DistributorId));
                cmd.Parameters.Add(new SqlParameter("SaleDate", salesData.SaleDate));
                cmd.Parameters.Add(new SqlParameter("ProductId", salesData.ProductId));
                cmd.Parameters.Add(new SqlParameter("UnitsSold", salesData.UnitsSold));
                cmd.Parameters.Add(new SqlParameter("TotalPrice", salesData.UnitsTotalPrice));
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
                SqlCommand cmd = new SqlCommand($"DELETE FROM SalesData WHERE SaleId = @SaleId");
                cmd.Parameters.Add(new SqlParameter("SaleId", salesData.SaleId));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public ISalesData GetSaleById(string SaleId)
        {
            List<ISalesData> salesData = new List<ISalesData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM SalesData WHERE SaleId = @SaleId");
                cmd.Parameters.Add(new SqlParameter("SaleId", SaleId));
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesData.Add(new SalesDataModel()
                        {
                            SaleId = reader["SaleId"].ToString(),
                            SaleDate = reader["SaleDate"].ToString(),
                            UnitsTotalPrice = Convert.ToDecimal(reader["UnitsTotalPrice"].ToString()),
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
        public List<ISalesData> GetSaleBetweenDatesAndId(int DistributorId, string FromDate, string ToDate)
        {
            List<ISalesData> salesData = new List<ISalesData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM SalesData WHERE DistributorId = @Id AND SaleDate >= @From AND SaleDate <= @Till");
                cmd.Parameters.Add(new SqlParameter("Id", DistributorId));
                cmd.Parameters.Add(new SqlParameter("From", FromDate));
                cmd.Parameters.Add(new SqlParameter("Till", ToDate));
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesData.Add(new SalesDataModel()
                        {
                            SaleId = reader["SaleId"].ToString(),
                            SaleDate = reader["SaleDate"].ToString(),
                            UnitsTotalPrice = Convert.ToDecimal(reader["UnitsTotalPrice"].ToString()),
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
        public List<ISalesData> GetSalesByUserId(int DistributorId)
        {
            List<ISalesData> salesData = new List<ISalesData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM SalesData WHERE DistributorId = @DistributorId");
                cmd.Parameters.Add(new SqlParameter("DistributorId", DistributorId));
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesData.Add(new SalesDataModel()
                        {
                            SaleId = reader["SaleId"].ToString(),
                            SaleDate = reader["SaleDate"].ToString(),
                            UnitsTotalPrice = Convert.ToDecimal(reader["UnitsTotalPrice"].ToString()),
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
                            SaleDate = reader["SaleDate"].ToString(),
                            UnitsTotalPrice = Convert.ToDecimal(reader["UnitsTotalPrice"].ToString()),
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
