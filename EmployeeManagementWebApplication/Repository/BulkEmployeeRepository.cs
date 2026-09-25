using EmployeeManagementWebApplication.Helpers;
using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace EmployeeManagementWebApplication.Repository
{
    public class BulkEmployeeRepository :IBulkEmployeeRepository
    {
        private readonly DataBaseConnection _dbConnection;
        public BulkEmployeeRepository(DataBaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
       public async Task BulkInsert(List<Employee> employees)
        {
            try
            {
                DataTable table = new DataTable();

                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Email", typeof(string));
                table.Columns.Add("Salary", typeof(decimal));

                foreach (Employee employee in employees)
                {
                    table.Rows.Add(employee.Name, employee.Email, employee.Salary);

                }
                using SqlConnection con = _dbConnection.GetConnection();
                await con.OpenAsync();
                using SqlBulkCopy bulkCopy = new SqlBulkCopy(con);
                bulkCopy.BatchSize = 1000;
                bulkCopy.BulkCopyTimeout = 60;
                bulkCopy.DestinationTableName = "Employee";
                bulkCopy.ColumnMappings.Add("Name", "Name");
                bulkCopy.ColumnMappings.Add("Email", "Email");
                bulkCopy.ColumnMappings.Add("Salary", "Salary");

                await bulkCopy.WriteToServerAsync(table);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
