using Microsoft.Data.SqlClient;

namespace EmployeeManagementWebApplication.Helpers
{
    public class DataBaseConnection
    {
        private readonly string _connectionString;

        public DataBaseConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
