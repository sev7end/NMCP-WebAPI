using NMCP.Abstractions.Models;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
{
    public class PaidSaleDbService
    {
        private readonly string _connectionString;
        public PaidSaleDbService()
        {
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(IPaidSales paidSale)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO PaidSales (ForId, SaleId) VALUES " +
                    "(@forId,@saleId)");
                cmd.Parameters.Add(new SqlParameter("forId", paidSale.ForId));
                cmd.Parameters.Add(new SqlParameter("saleId", paidSale.SaleId));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        
        public void DeleteItemInCollection(IPaidSales paidSale)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM PaidSales WHERE ForId = @forId");
                cmd.Parameters.Add(new SqlParameter("forId", paidSale.ForId));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public bool CheckIfSaleExists(int forId, string saleId)
        {
            List<IPaidSales> paidSale = new List<IPaidSales>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM PaidSales WHERE ForId = @forId AND SaleId = @saleId");
                cmd.Parameters.Add(new SqlParameter("forId", forId));
                cmd.Parameters.Add(new SqlParameter("saleId", saleId));
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paidSale.Add(new PaidSalesModel()
                        {
                            ForId = Convert.ToInt32(reader["ForId"]),
                            SaleId = reader["SaleId"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return paidSale.Count != 0 ? true : false;
        }
    }
}
