using EmployeeManagementWebApplication.Helpers;
using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementWebApplication.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly DataBaseConnection _dbConnection;

        public UserRepository(DataBaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<User>Login(LoginRequest request)
        {
            User user = null;
            try
            {
                

                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand("LoginUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", request.Name);
                    cmd.Parameters.AddWithValue("@Password", request.Password);

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString(),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                    }

                    await reader.CloseAsync();
                }

                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }
    }
}
    
