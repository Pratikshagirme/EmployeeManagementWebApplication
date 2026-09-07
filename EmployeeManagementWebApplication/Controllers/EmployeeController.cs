using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmployeeManagementWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly ILogger<EmployeeController> _Logger;
        public EmployeeController(IEmployeeRepository repository,ILogger<EmployeeController>Logger)
        {
            _repository = repository;
            _Logger = Logger;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            

            _Logger.LogInformation("Fetching all employees");

            CommonResponse response =await _repository.GetAllEmployees();

            _Logger.LogInformation("Fetched all employees successfully");

            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            _Logger.LogInformation("Fetching employees with {Id}",Id);
            CommonResponse response = await _repository.GetEmployeeById(Id);
            if (response.Data == null)
            {
                _Logger.LogWarning("Employee with Id { Id} is not found", Id);
            }
            else
            {
                _Logger.LogInformation("Employee with Id {Id} id succesfuuly fetched",Id);
            }
            
                return Ok(response);
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InsertEmployee(EmployeeRequest request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                
                Employee employee = new Employee()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Department = request.Department,
                    Salary = request.Salary

                };
                response = await _repository.InsertEmployee(employee);

                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }catch(Exception ex)
            {
                _Logger.LogError(ex, "Error are occured while inserting");
                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message = ex.Message,
                    Data = null
                };
            }

            finally{}
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult>UpdateEmployee(EmployeeRequest request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _Logger.LogInformation("Update employee{Id}", request.Id);
                Employee employee = new Employee()
                {
                    Id = request.Id ?? 0,
                    Name = request.Name,
                    Email = request.Email,
                    Department = request.Department,
                    Salary = request.Salary
                };
                 response = await _repository.UpdateEmployee(employee);
                if (response.StatusCode == 201)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
                _Logger.LogInformation("Update employee succesfull{Id}", request.Id);

            }
            catch(Exception ex){
                _Logger.LogError(ex, "Error are occured while updating");
                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message = ex.Message,
                    Data = null
                };

            }
            
            finally { }
            return Ok(response);
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                
                response = await _repository.DeleteEmployee(Id);

                if (response.StatusCode == 201)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);

                }


            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while deleting employee with ID {Id}", Id);

                response = new CommonResponse
                {
                    StatusCode = 202,
                    Message = "Something went wrong",
                    Data = null
                };
            }
            finally { }
            return Ok();
        }
        [HttpPost("Upload-Image")]
        public async Task<IActionResult>UploadEmployeeImage(ImageUploadRequest request)
        {
            try
            {
                CommonResponse response = new CommonResponse();
                response = await _repository.UploadEmployeeImage(request);
                return Ok(response);
            }
            finally { }
        }
        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("This is a test exception");
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            CommonResponse response = await _repository.Register(request);

            return Ok(response);
        }


    }
}
