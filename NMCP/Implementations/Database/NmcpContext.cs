using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
{
    public class NmcpContext
    {
        private string connectionString;
        public NmcpContext()
        {
            connectionString = AppSettings.MSAccessDBPath;
        }
       
        public void Template()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("", conn);

                

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
