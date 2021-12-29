using NMCP.Core.Models;
using NMCP.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Data
{
    public class AuthDbService
    {
        private readonly string _connectionString;
        public AuthDbService()
        {
            _connectionString = SqlConnectionManager.ConnectionString;
        }

        public void AddItemToCollection(IDistributorAuth authData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO DistributorAuth (Id, Email, UserName, SHA512Pwd, TempPwd) VALUES " +
                    "(@Id,@Email,@UserName,@Pwd, '-')");
                cmd.Parameters.Add(new SqlParameter("Id",authData.Id));
                cmd.Parameters.Add(new SqlParameter("Email",authData.Email));
                cmd.Parameters.Add(new SqlParameter("UserName",authData.UserName));
                cmd.Parameters.Add(new SqlParameter("Pwd",authData.SHA512Pwd));

                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateItemInCollection(IDistributorAuth authData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE DistributorAuth SET Email = @Email, UserName = @UserName, SHA512Pwd = @Pwd, TempPwd = @TPwd WHERE Id = @Id");
                cmd.Parameters.Add(new SqlParameter("Id", authData.Id));
                cmd.Parameters.Add(new SqlParameter("Email", authData.Email));
                cmd.Parameters.Add(new SqlParameter("UserName", authData.UserName));
                cmd.Parameters.Add(new SqlParameter("Pwd", authData.SHA512Pwd));
                cmd.Parameters.Add(new SqlParameter("TPwd", authData.TempPwd));
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void DeleteItemInCollection(IDistributorAuth authData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM DistributorAuth WHERE Id = {authData.Id}");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public IDistributorAuth GetAuthInfoById(int Id)
        {
            List<IDistributorAuth> authData = new List<IDistributorAuth>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM DistributorAuth WHERE Id = {Id}");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        authData.Add(new DistributorAuthModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Email = reader["Email"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            SHA512Pwd = reader["SHA512Pwd"].ToString(),
                            TempPwd = reader["TempPwd"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return authData.First();
        }
        /*
        public List<IDistributorAuth> GetProducts()
        {
            List<IDistributorAuth> referalData = new List<IDistributorAuth>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE * FROM ProductData");
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        referalData.Add(new DistributorAuthModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Email = reader["Email"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            SHA512Pwd = reader["SHA512Pwd"].ToString(),
                            TempPwd = reader["TempPwd"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return referalData;
        }
        */
    }
}
