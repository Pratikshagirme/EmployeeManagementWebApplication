using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using EmployeeManagementWebApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkEmployeeController : ControllerBase
    {
        private readonly IBulkEmployeeRepository _repository;
        public BulkEmployeeController(IBulkEmployeeRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> BulkInsert(List<Employee> employees)
        {
            await _repository.BulkInsert(employees);
            return Ok("Employees insert succesffull");
        }
    }
}
