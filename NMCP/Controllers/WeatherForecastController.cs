using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NMCP.Implementations.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [Route("hello")]
        public string ConnStatus()
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionManager.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO DistributorData (LastName, FirstName, DateOfBirth, Gender, PictureUrl, IsVerifiedUser,ContactType,ContactInfo,AddressType,UserAddress) VALUES ('Cardinal', 'Tom B. Erichsen', '22/01/2000', '0', 'someurl', '0', '1','555111222','1','BakerStreet 51 ave')");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return "test";
        }
    }
}
