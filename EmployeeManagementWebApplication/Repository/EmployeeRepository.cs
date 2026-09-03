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

        public EmployeeRepository(DataBaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<CommonResponse> GetAllEmployees()
        {
            CommonResponse response = new CommonResponse();
            try
            {
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

                        employees.Add(employee);
                    }

                }
                if (employees.Count > 0)
                {
                    response = new CommonResponse
                    {
                        StatusCode = 200,
                        Message = "Data Fetched successfully",
                        Data = employees

                    };
                }
                else
                {
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
                        
                        response = new CommonResponse
                        {
                            StatusCode = 200,
                            Message ="Data fetched successfully.",
                            Data = employee
                        };
                    }
                    else
                    {
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
            }
            catch (Exception ex)
            {
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
            }
            catch (Exception ex)
            {
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
            }
            catch (Exception ex)
            {
                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message = ex.Message
                };
            }
            return response;
        }

    }
}
