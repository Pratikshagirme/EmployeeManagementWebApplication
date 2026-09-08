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

        public async Task<CommonResponse> Register(RegisterRequest request)
        {
            CommonResponse response = new CommonResponse();

            try
            {
                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand("RegisterUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", request.Name);
                    cmd.Parameters.AddWithValue("@Email", request.Email);
                    cmd.Parameters.AddWithValue("@Password", request.Password);
                    cmd.Parameters.AddWithValue("@RoleName", request.RoleName);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.StatusCode = Convert.ToInt32(reader["StatusCode"]);
                            response.Message = reader["Message"].ToString();
                            response.Data = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }


        public async Task<UserLoginData> LoginUser(LoginRequest request)
        {
            using (SqlConnection con = _dbConnection.GetConnection())
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand("LoginUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", request.Email);
                cmd.Parameters.AddWithValue("@Password", request.Password);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserLoginData
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            RoleName = reader["RoleName"].ToString()
                        };
                    }
                }
            }

            return null;
        }
        public async Task<CommonResponse> GetUserProfile(int userId)
        {
            CommonResponse response = new CommonResponse();

            try
            {
                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand("GetUserProfile", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.StatusCode = 200;
                            response.Message = "Profile fetched successfully";

                            response.Data = new
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                ProfileImage = reader["ProfileImage"] == DBNull.Value? null: reader["ProfileImage"].ToString(),
                                UserId = Convert.ToInt32(reader["UserId"])
                            };
                        }
                        else
                        {
                            response.StatusCode = 400;
                            response.Message = "Employee profile not found";
                            response.Data = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }
        public async Task<CommonResponse> UpdateUserProfile(int userId,string name,string email)
        {
            CommonResponse response = new CommonResponse();

            try
            {
                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand(
                        "UpdateUserProfile",
                        con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.StatusCode =
                                Convert.ToInt32(reader["StatusCode"]);

                            response.Message =
                                reader["Message"].ToString();

                            response.Data = null;
                        }
                        else
                        {
                            response.StatusCode = 500;
                            response.Message = "Profile update failed";
                            response.Data = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }
        public async Task<CommonResponse> LinkEmployeeToUser(int employeeId,int userId)
        {
            CommonResponse response = new CommonResponse();

            try
            {
                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand("LinkEmployeeToUser",con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader =await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.StatusCode =Convert.ToInt32(reader["StatusCode"]);

                            response.Message =reader["Message"].ToString();

                            response.Data = null;
                        }
                        else
                        {
                            response.StatusCode = 500;
                            response.Message = "Employee linking failed";
                            response.Data = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }
    }
}