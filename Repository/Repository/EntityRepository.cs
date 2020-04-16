using Repository.Repository.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public abstract class EntityRepository
    {
        protected async Task<SqlConnection> GetConnection()
        {
            SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString);
            
            return connection;
        }
        protected SqlCommand CreateCommand(string command, SqlConnection connection)
        {
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            return sqlCommand;
        }
    }
}
