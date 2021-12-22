using NMCP.Abstractions.Models;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
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
                    "(Id, ReferallId,  ReferedUsers, ReferallLevel) VALUES" +
                    $"({referalData.ReferalId}, {referalData.ReferalId},{referalData.ReferedUsers},{referalData.ReferallLevel}");
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
                SqlCommand cmd = new SqlCommand($"UPDATE ReferalData SET" +
                    $"ReferedUsers = {referalData.ReferedUsers} WHERE Id = {referalData.Id}");
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
                SqlCommand cmd = new SqlCommand($"DELETE FROM ReferalData WHERE Id = {referalData.Id}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public IReferalData GetReferalById(int Id)
        {
            List<IReferalData> referalData = new List<IReferalData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT FROM ReferalData WHERE Id = {Id}");
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
            return referalData.First();
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
