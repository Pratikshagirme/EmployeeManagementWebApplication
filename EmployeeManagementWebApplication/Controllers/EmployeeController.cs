using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            CommonResponse response = await _repository.GetAllEmployees();
            
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            CommonResponse response = await _repository.GetEmployeeById(Id);
            
                return Ok(response);
            
        }
        [HttpPost]
        public async Task<IActionResult> InsertEmployee(EmployeeRequest request)
        {
            try
            {
                CommonResponse response = new CommonResponse();
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
            }
            finally{}
        }
        [HttpPut]
        public async Task<IActionResult>UpdateEmployee(EmployeeRequest request)
        {
            try
            {
                CommonResponse response = new CommonResponse();
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

            }
            finally { }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {
                CommonResponse response = new CommonResponse();
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
            finally { }
        }
    }
}
