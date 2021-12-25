using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NMCP.Abstractions.Models;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
{
    public class WalletDbService : ControllerBase
    {
        private readonly TransactionDbService transactionDbService;
        private readonly string _connectionString;
        public WalletDbService()
        {
            transactionDbService = new TransactionDbService();
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(IDistributorWallet walletData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO DistributorWallet (DistributorId, WalletMoney) VALUES " +
                    "(@Id,@Money)");
                cmd.Parameters.Add(new SqlParameter("Id", walletData.DistributorId));
                cmd.Parameters.Add(new SqlParameter("Money", walletData.WalletMoney));

                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateItemInCollection(IDistributorWallet walletData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE DistributorWallet SET WalletMoney = @Money WHERE DistributorId = @Id");
                cmd.Parameters.Add(new SqlParameter("Id", walletData.DistributorId));
                cmd.Parameters.Add(new SqlParameter("Money", walletData.WalletMoney));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void DeleteItemInCollection(IDistributorWallet walletData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM DistributorWallet WHERE DistributorId = @Id");
                cmd.Parameters.Add(new SqlParameter("Id", walletData.DistributorId));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public IDistributorWallet GetDistributorWallet(int Id)
        {
            List<IDistributorWallet> walletData = new List<IDistributorWallet>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM DistributorWallet WHERE DistributorId = @Id");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        walletData.Add(new DistributorWalletModel()
                        {
                            DistributorId = Convert.ToInt32(reader["Id"]),
                            WalletMoney = Convert.ToDecimal(reader["Money"])
                           
                        });
                    }
                }
                conn.Close();
            }
            walletData.First().transactions = transactionDbService.GetTransactionsForId(Id);
            return walletData.First();
        }
    }
}
