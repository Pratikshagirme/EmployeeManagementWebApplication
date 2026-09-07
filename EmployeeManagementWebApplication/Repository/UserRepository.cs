using EmployeeManagementWebApplication.Helpers;
using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementWebApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseConnection _dbConnection;

        public UserRepository(DataBaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<User> Login(LoginRequest request)
        {
            User user = null;

            CommonResponse response = new CommonResponse();
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
                        response.StatusCode = 200;
                        response.Message = "Login successful";
                        response.Data = user;
                    }

                    
                    else {
                        response.StatusCode = 401;
                        response.Message = "Invalid username or password";
                        response.Data = null;
                    }
                    await reader.CloseAsync();
                }


            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "there is internal server error";
                response.Data = null;
            }
            return user;
        }
    }
}
