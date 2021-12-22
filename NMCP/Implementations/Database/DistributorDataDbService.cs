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
    public class DistributorDataDbService
    {
        private readonly string _connectionString;
        public DistributorDataDbService()
        {
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(IDistributorData distributorData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand($"INSERT INTO DistributorData"+
                    "(LastName, FirstName, DateOfBirth, Gender, PictureUrl, IsVerifiedUser,ContactType,ContactInfo,AddressType,UserAddress) VALUES"+
                    $"({distributorData.LastName} , {distributorData.FirstName}, {distributorData.DateOfBirth}, "+
                    $"{(int)distributorData.Gender}, {distributorData.PictureUrl}, {distributorData.IsUserVerified},"+
                    $" {(int)distributorData.contactType},{distributorData.ContactInfo},{(int)distributorData.addressType},{distributorData.UserAddress}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateItemInCollection(IDistributorData distributorData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE DistributorData SET LastName = {distributorData.LastName}"+
                    $", FirstName = {distributorData.FirstName}, DateOfBirth = {distributorData.DateOfBirth}, Gender = {(int)distributorData.Gender} " +
                    $", PictureUrl = {distributorData.PictureUrl} , IsVerifiedUser = {distributorData.IsUserVerified},ContactType={(int)distributorData.contactType}," +
                    $"ContactInfo={distributorData.ContactInfo}, AddressType={(int)distributorData.addressType}, UserAddres={distributorData.UserAddress}" +
                    $" WHERE Id = {distributorData.Id}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void DeleteItemInCollection(IDistributorData distributorData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM DistributorData WHERE Id = {distributorData.Id}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public IDistributorData GetReferalById(int Id)
        {
            List<IDistributorData> distributorData = new List<IDistributorData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT FROM DistributorData WHERE Id = {Id}");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        distributorData.Add(new DistributorDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            Gender = (GenderType)Convert.ToInt32(reader["Gender"]),
                            DateOfBirth = reader["DateOfBirth"].ToString(),
                            PictureUrl = reader["PictureUrl"].ToString(),
                            IsUserVerified = Convert.ToBoolean(reader["IsUserVerified"]),
                            contactType = (ContactType)Convert.ToInt32(reader["ContactType"]),
                            ContactInfo = reader["ContactInfo"].ToString(),
                            addressType = (AddressType)Convert.ToInt32(reader["AddressType"]),
                            UserAddress = reader["UserAddress"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return distributorData.First();
        }
        public List<IDistributorData> GetReferals()
        {
            List<IDistributorData> distributorData = new List<IDistributorData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM DistributorData");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        distributorData.Add(new DistributorDataModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            Gender = (GenderType)Convert.ToInt32(reader["Gender"]),
                            DateOfBirth = reader["DateOfBirth"].ToString(),
                            PictureUrl = reader["PictureUrl"].ToString(),
                            IsUserVerified = Convert.ToBoolean(reader["IsUserVerified"]),
                            contactType = (ContactType)Convert.ToInt32(reader["ContactType"]),
                            ContactInfo = reader["ContactInfo"].ToString(),
                            addressType = (AddressType)Convert.ToInt32(reader["AddressType"]),
                            UserAddress = reader["UserAddress"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return distributorData;
        }
    }
}
