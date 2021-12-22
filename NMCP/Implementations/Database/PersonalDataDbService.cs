using NMCP.Abstractions.DataTypes;
using NMCP.Abstractions.Models;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
{
    public class PersonalDataDbService
    {
        private readonly string _connectionString;
        public PersonalDataDbService()
        {
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(IDistributorPersonalData distributorData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO DistributorPersonalData" +
                    "(Id, DocumentType, DocumentNumber, DocumentSerial, IssueDate, ExpirtyDate, PrivateNumber,IssuingAgency) VALUES" +
                    $"({distributorData.Id} , {(int)distributorData.documentType}, {distributorData.DocumentSerial}, " +
                    $"{distributorData.IssueDate}, {distributorData.ExpirtyDate}, {distributorData.PrivateNumber}," +
                    $" {distributorData.IssuingAgency}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateItemInCollection(IDistributorPersonalData distributorData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE DistributorPersonalData SET" +
                    $" DocumentType = {(int)distributorData.documentType}, DocumentNumber = {distributorData.DocumentNumber}, DocumentSerial = {distributorData.DocumentSerial} " +
                    $", IssueDate = {distributorData.IssueDate} , ExpirtyDate = {distributorData.ExpirtyDate},PrivateNumber={distributorData.PrivateNumber}," +
                    $"IssuingAgency = {distributorData.IssuingAgency} WHERE Id = {distributorData.Id}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void DeleteItemInCollection(IDistributorPersonalData distributorData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM DistributorPersonalData WHERE Id = {distributorData.Id}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public IDistributorPersonalData GetDistributorPersonalDataById(int Id)
        {
            List<IDistributorPersonalData> distributorData = new List<IDistributorPersonalData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT FROM DistributorPersonalData WHERE Id = {Id}");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        distributorData.Add(new DistributorPersonalDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            documentType = (DocumentType)Convert.ToInt32(reader["DocumentType"]),
                            DocumentNumber = reader["ExpirtyDate"].ToString(),
                            ExpirtyDate = reader["ExpirtyDate"].ToString(), 
                            IssueDate = reader["IssueDate"].ToString(),
                            IssuingAgency = reader["IssuingAgency"].ToString(),
                            DocumentSerial = reader["DocumentSerial"].ToString(),
                            PrivateNumber = reader["PrivateNumber"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return distributorData.First();
        }
        public List<IDistributorPersonalData> GetDistributorPersonalDatas()
        {
            List<IDistributorPersonalData> distributorData = new List<IDistributorPersonalData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM DistributorPersonalData");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        distributorData.Add(new DistributorPersonalDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            documentType = (DocumentType)Convert.ToInt32(reader["DocumentType"]),
                            DocumentNumber = reader["ExpirtyDate"].ToString(),
                            ExpirtyDate = reader["ExpirtyDate"].ToString(),
                            IssueDate = reader["IssueDate"].ToString(),
                            IssuingAgency = reader["IssuingAgency"].ToString(),
                            DocumentSerial = reader["DocumentSerial"].ToString(),
                            PrivateNumber = reader["PrivateNumber"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return distributorData;
        }
    }
}
