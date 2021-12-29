using NMCP.Core.Enums;
using NMCP.Core.Models;
using NMCP.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Data
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

                SqlCommand cmd = new SqlCommand($"INSERT INTO DistributorData" +
                    "(LastName, FirstName, DateOfBirth, Gender, PictureUrl, IsVerifiedUser,ContactType,ContactInfo,AddressType,UserAddress,UniqueId) VALUES" +
                   "(@LastName,@FirstName, @DateOfBirth, @Gender, @PictureUrl, @IsUserVerified,@ContactType,@ContactInfo,@AddressType,@UserAddress,@UniqueId)");
                cmd.Parameters.Add(new SqlParameter("LastName", distributorData.LastName));
                cmd.Parameters.Add(new SqlParameter("FirstName", distributorData.FirstName));
                cmd.Parameters.Add(new SqlParameter("DateOfBirth", distributorData.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("Gender", (int)distributorData.Gender));
                cmd.Parameters.Add(new SqlParameter("PictureUrl", distributorData.PictureUrl));
                cmd.Parameters.Add(new SqlParameter("IsUserVerified",distributorData.IsUserVerified));
                cmd.Parameters.Add(new SqlParameter("ContactType", (int)distributorData.contactType));
                cmd.Parameters.Add(new SqlParameter("ContactInfo",distributorData.ContactInfo));
                cmd.Parameters.Add(new SqlParameter("AddressType", (int)distributorData.addressType));
                cmd.Parameters.Add(new SqlParameter("UserAddress", distributorData.UserAddress));
                cmd.Parameters.Add(new SqlParameter("UniqueId", distributorData.UniqueId));
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
                SqlCommand cmd = new SqlCommand("UPDATE DistributorData SET LastName = @LastName"+
                    ", FirstName = @FirstName, DateOfBirth = @DateOfBirth, Gender = @Gender " +
                    ", PictureUrl = @PictureUrl , IsVerifiedUser = @IsVerifiedUser,ContactType=@ContactType," +
                    "ContactInfo=@ContactInfo, AddressType=@AddressType, UserAddress=@UserAddress" +
                    $" WHERE Id = {distributorData.Id}");
                cmd.Parameters.Add(new SqlParameter("LastName", distributorData.LastName));
                cmd.Parameters.Add(new SqlParameter("FirstName", distributorData.FirstName));
                cmd.Parameters.Add(new SqlParameter("DateOfBirth", distributorData.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("Gender", (int)distributorData.Gender));
                cmd.Parameters.Add(new SqlParameter("PictureUrl", distributorData.PictureUrl));
                cmd.Parameters.Add(new SqlParameter("IsUserVerified", distributorData.IsUserVerified));
                cmd.Parameters.Add(new SqlParameter("ContactType", (int)distributorData.contactType));
                cmd.Parameters.Add(new SqlParameter("ContactInfo", distributorData.ContactInfo));
                cmd.Parameters.Add(new SqlParameter("AddressType", (int)distributorData.addressType));
                cmd.Parameters.Add(new SqlParameter("UserAddress", distributorData.UserAddress));
                cmd.Parameters.Add(new SqlParameter("UniqueId", distributorData.UniqueId));
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
                SqlCommand cmd = new SqlCommand("DELETE * FROM [NetworkMarketingCP].[dbo].[DistributorData] WHERE Id = @id");
                cmd.Parameters.Add(new SqlParameter("id", distributorData.Id));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public IDistributorData GetDistributorDataByUniqueId(string Id)
        {
            List<IDistributorData> distributorData = new List<IDistributorData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [NetworkMarketingCP].[dbo].[DistributorData] WHERE UniqueId = @UniqueId");
                cmd.Parameters.Add(new SqlParameter("UniqueId", Id));
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
                            IsUserVerified = Convert.ToBoolean(Convert.ToInt32(reader["IsVerifiedUser"])),
                            contactType = (ContactType)Convert.ToInt32(reader["ContactType"]),
                            ContactInfo = reader["ContactInfo"].ToString(),
                            addressType = (AddressType)Convert.ToInt32(reader["AddressType"]),
                            UserAddress = reader["UserAddress"].ToString(),
                            UniqueId = reader["UniqueId"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return distributorData.First();
        }
        public IDistributorData GetDistributorDataById(int Id)
        {
            List<IDistributorData> distributorData = new List<IDistributorData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM DistributorData WHERE Id = {Id}");
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
                            IsUserVerified = Convert.ToBoolean(reader["IsVerifiedUser"]),
                            contactType = (ContactType)Convert.ToInt32(reader["ContactType"]),
                            ContactInfo = reader["ContactInfo"].ToString(),
                            addressType = (AddressType)Convert.ToInt32(reader["AddressType"]),
                            UserAddress = reader["UserAddress"].ToString(),
                            UniqueId = reader["UniqueId"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return distributorData.Count != 0 ? distributorData.First() : null;
        }
        public List<IDistributorData> GetDistributorDatas()
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
                            IsUserVerified = Convert.ToBoolean(reader["IsVerifiedUser"]),
                            contactType = (ContactType)Convert.ToInt32(reader["ContactType"]),
                            ContactInfo = reader["ContactInfo"].ToString(),
                            addressType = (AddressType)Convert.ToInt32(reader["AddressType"]),
                            UserAddress = reader["UserAddress"].ToString(),
                            UniqueId = reader["UniqueId"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return distributorData;
        }
    }
}
