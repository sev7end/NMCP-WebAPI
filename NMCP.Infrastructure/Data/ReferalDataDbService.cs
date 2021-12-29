using NMCP.Core.Models;
using NMCP.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Data
{
    public class ReferalDataDbService
    {
        private readonly string _connectionString;
        public ReferalDataDbService()
        {
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(IReferalData referalData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO ReferalData" +
                    "(Id, ReferallId,  ReferedUsers, ReferallLevel) VALUES " +
                    "(@Id, @ReferalId, @ReferedUsers, @ReferalLevel)");
                cmd.Parameters.Add(new SqlParameter("Id",referalData.Id));
                cmd.Parameters.Add(new SqlParameter("ReferalId",referalData.ReferalId));
                cmd.Parameters.Add(new SqlParameter("ReferedUsers",referalData.ReferedUsers));
                cmd.Parameters.Add(new SqlParameter("ReferalLevel",referalData.ReferallLevel));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateItemInCollection(IReferalData referalData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE ReferalData SET " +
                    $"ReferedUsers = @ReferedUsers WHERE Id = @Id");
                cmd.Parameters.Add(new SqlParameter("Id", referalData.Id));
                cmd.Parameters.Add(new SqlParameter("ReferalId", referalData.ReferalId));
                cmd.Parameters.Add(new SqlParameter("ReferedUsers", referalData.ReferedUsers));
                cmd.Parameters.Add(new SqlParameter("ReferalLevel", referalData.ReferallLevel));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void DeleteItemInCollection(IReferalData referalData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM ReferalData WHERE Id = @Id");
                cmd.Parameters.Add(new SqlParameter("Id", referalData.Id));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public List<int> GetDistributorsReferalsByIdAndLevel(int Id, int Level)
        {
            List<int> Ids = new List<int>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM ReferalData WHERE ReferallId = @Id AND ReferallLevel = @Level");
                cmd.Parameters.Add(new SqlParameter("Id", Id));
                cmd.Parameters.Add(new SqlParameter("Level", Level));
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ids.Add(Convert.ToInt32(reader["Id"]));
                    }
                }
                conn.Close();
            }
            return Ids.Count != 0 ? Ids : null;
        }
        public IReferalData GetReferalById(int Id)
        {
            List<IReferalData> referalData = new List<IReferalData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM ReferalData WHERE Id = @Id");
                cmd.Parameters.Add(new SqlParameter("Id", Id));
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        referalData.Add(new ReferalDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ReferalId = Convert.ToInt32(reader["ReferallId"]),
                            ReferedUsers = Convert.ToInt32(reader["ReferedUsers"]),
                            ReferallLevel = Convert.ToInt32(reader["ReferallLevel"])
                        });
                    }
                }
                conn.Close();
            }
            return referalData.Count!= 0 ? referalData.First() : null;
        }
        public List<IReferalData> GetReferals()
        {
            List<IReferalData> referalData = new List<IReferalData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM ReferalData");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        referalData.Add(new ReferalDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ReferalId = Convert.ToInt32(reader["ReferallId"]),
                            ReferedUsers = Convert.ToInt32(reader["ReferedUsers"]),
                            ReferallLevel = Convert.ToInt32(reader["ReferallLevel"])
                        });
                    }
                }
                conn.Close();
            }
            return referalData;
        }
    }
}
