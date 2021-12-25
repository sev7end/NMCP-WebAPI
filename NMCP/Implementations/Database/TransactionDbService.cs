using NMCP.Abstractions.Models;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
{
    public class TransactionDbService
    {
        private readonly string _connectionString;
        public TransactionDbService()
        {
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(ITransaction transactionData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO TransactionData (TowardsId, TransactionDate,Amount) VALUES " +
                    "(@TowardsId, @Date,@Amount)");
                cmd.Parameters.Add(new SqlParameter("TowardsId", transactionData.TowardsId));
                cmd.Parameters.Add(new SqlParameter("Date", transactionData.TransactionDate));
                cmd.Parameters.Add(new SqlParameter("Amount", transactionData.Amount));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
       
        public List<ITransaction> GetTransactionsForId(int Id)
        {
            List<ITransaction> walletData = new List<ITransaction>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM TransactionData WHERE TowardsId = @Id");
                cmd.Parameters.Add(new SqlParameter("TowardsId", Id));
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        walletData.Add(new TransactionModel()
                        {
                            TowardsId = Convert.ToInt32(reader["TowardsId"]),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            TransactionDate = reader["Amount"].ToString()

                        });
                    }
                }
                conn.Close();
            }
            return walletData;
        }
    }
}
