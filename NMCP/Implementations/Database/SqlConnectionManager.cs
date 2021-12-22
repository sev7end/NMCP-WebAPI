using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Database
{
    public class SqlConnectionManager
    {
        public static string ConnectionString { get; set; }
        public SqlConnectionManager(string _connectionString)
        {
            ConnectionString = _connectionString;
        }
       
        private SqlConnection EstablishConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
