using EmployeeManagementWebApplication.Helpers;
using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using Microsoft.Data.SqlClient;
using System.Data;
namespace EmployeeManagementWebApplication.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataBaseConnection _dbConnection;
        private readonly ILogger<EmployeeRepository> _Logger;

        public EmployeeRepository(DataBaseConnection dbConnection,ILogger<EmployeeRepository> Logger)
        {
            _dbConnection = dbConnection;
            _Logger = Logger;
        }

        public async Task<CommonResponse> GetAllEmployees()
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _Logger.LogInformation("Fetchin all employees from database");
                List<Employee> employees = new List<Employee>();
                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("GetAllEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    DataTable table = dataset.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        Employee employee = new Employee();
                        employee.Id = Convert.ToInt32(row["Id"]);
                        employee.Name = row["Name"].ToString();
                        employee.Email = row["Email"].ToString();
                        employee.Department = row["Department"].ToString();
                        employee.Salary = Convert.ToDecimal(row["Salary"]);
                        employee.IsActive = Convert.ToBoolean(row["IsActive"]);
                        employee.ProfileImage = row["ProfileImage"] == DBNull.Value ? null : row["ProfileImage"].ToString();

                        employees.Add(employee);
                    }

                }
                if (employees.Count > 0)
                {
                    _Logger.LogInformation("successfully fetcheched {count} employees from database",employees.Count);
                    response = new CommonResponse
                    {
                        StatusCode = 200,
                        Message = "Data Fetched successfully",
                        Data = employees

                    };
                }
                else
                {
                    _Logger.LogWarning("No employees found in database");
                    response = new CommonResponse
                    {
                        StatusCode = 204,
                        Message = "Data not found",
                        Data = employees

                    };
                }
                

            }
            catch (Exception ex)
            {
                _Logger.LogWarning(ex, "Error Occured while fetching all employees");
                response = new CommonResponse
                {
                    StatusCode = 500,
                    Message = "Something went wrong",
                    Data = ex.Message
                };
            }
            return response;
        }
        public async Task<CommonResponse> GetEmployeeById(int Id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _Logger.LogInformation("Fetching employee with Id {Id} from database",Id);
                DataTable table = new DataTable();
                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("GetEmployeeById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);    
                    Employee employee = null;
                    if(table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            employee = new Employee();
                            employee.Id = Convert.ToInt32(row["Id"]);
                            employee.Name = row["Name"].ToString();
                            employee.Email = row["Email"].ToString();
                            employee.Department = row["Department"].ToString();
                            employee.Salary = Convert.ToDecimal(row["Salary"]);
                            employee.IsActive = Convert.ToBoolean(row["IsActive"]);
                        }
                        _Logger.LogInformation("Employee with Id {Id} fetched successfully",Id);

                        response = new CommonResponse
                        {
                            StatusCode = 200,
                            Message ="Data fetched successfully.",
                            Data = employee
                        };
                    }
                    else
                    {
                        _Logger.LogWarning("Employee with Id {Id} was not found in database",Id);
                        response = new CommonResponse
                        {
                            StatusCode = 204,
                            Message = "Data Not found!",
                            Data = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex,"Error occurred while fetching employee with Id {Id}",Id);
                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message = ex.Message,
                    Data = null
                };
            }
            return response;
        }
        public async Task<CommonResponse> InsertEmployee(Employee employee)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _Logger.LogInformation("Inserting employee with email {Email}",employee.Email);
                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("InsertEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    DataTable table = dataset.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        
                        response = new CommonResponse
                        {
                            StatusCode = Convert.ToInt32(row["StatusCode"]),
                            Message = Convert.ToString(row["Message"]),
                            Data = null
                        };
                    }
                    
                }
                _Logger.LogInformation("Employee insertion completed for email {Email}", employee.Email);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while inserting employee with email {Email}",employee.Email);
                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message = ex.Message
                };
            }
            return response;
        }
        public async Task<CommonResponse> UpdateEmployee(Employee employee)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _Logger.LogInformation("Updating employee with Id {Id}",employee.Id);
                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", employee.Id);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    DataTable tables = dataset.Tables[0];
                    foreach (DataRow row in tables.Rows)
                    {
                        response = new CommonResponse
                        {
                            StatusCode = Convert.ToInt32(row["StatusCode"]),
                            Message = Convert.ToString(row["Message"]),
                            Data = null
                        };
                    }
                    

                }
                _Logger.LogInformation("Employee with Id {Id} update operation completed", employee.Id);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex,"Error occurred while updating employee with Id {Id}",employee.Id);
                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message=ex.Message
                };
            }
            return response;
        }

        public async Task<CommonResponse> DeleteEmployee(int Id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _Logger.LogInformation("Deleting employee with Id {Id}",Id);

                using (SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    DataTable table = dataset.Tables[0];

                    foreach (DataRow row in table.Rows)
                    {
                        response = new CommonResponse
                        {
                            StatusCode = Convert.ToInt32(row["StatusCode"]),
                            Message = Convert.ToString(row["Message"]),
                            Data = null
                        };

                    }
                    
                }
                _Logger.LogInformation("Employee with Id {Id} delete operation completed",Id);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex,"Error occurred while deleting employee with Id {Id}",Id);
                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message = ex.Message
                };
            }
            return response;
        }
        public async Task<CommonResponse>UploadEmployeeImage(ImageUploadRequest request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                if (request.Image == null || request.Image.Length == 0)
                {
                    response = new CommonResponse
                    {
                        StatusCode = 204,
                        Message = "Please select image",
                        Data = null
                    };
                    return response;
                }
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "employee");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string extension = Path.GetExtension(request.Image.FileName);
                string fileName = $"employee_{request.EmployeeId}{extension}";
                string filePath = Path.Combine(folderPath, fileName);
                using(FileStream stream=new FileStream(
                    filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }
                string imagePath = $"/uploads/employee/{fileName}";

                using(SqlConnection con = _dbConnection.GetConnection())
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("UpdateEmployeeProfileImage", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", request.EmployeeId);
                    cmd.Parameters.AddWithValue("@ProfileImage", imagePath);
                    await cmd.ExecuteNonQueryAsync();
                }
                response = new CommonResponse
                {
                    StatusCode = 200,
                    Message = "Employee profile image uploaded successfully.",
                    Data = imagePath
                };
            }
            catch(Exception ex)
            {
                _Logger.LogError( ex,"Error uploading profile image for employee {Id}",request.EmployeeId);
                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message = "Something went wrong.",
                    Data = null
                };
            }
            return response;
        }

    }
}
