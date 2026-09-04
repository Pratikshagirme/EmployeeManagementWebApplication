using EmployeeManagementWebApplication.Helpers;
using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using EmployeeManagementWebApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmployeeManagementWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtTokenHelper _jwtTokenHelper;
        public AuthController(IUserRepository repository,JwtTokenHelper jwtTokenHelper)
        {
            _repository = repository;
            _jwtTokenHelper = jwtTokenHelper;
        }
        [HttpPost("Login")]
        public async Task<IActionResult>Login(LoginRequest request)
        {
            try
            {
               
                CommonResponse response = new CommonResponse();
                
                User user = await _repository.Login(request);
                if (user == null)
                {
                    response = new CommonResponse
                    {
                        StatusCode = 204,
                        Message = "Invalid username or password",
                        Data = null
                    };
                    return Ok(response);
                }
                string token = _jwtTokenHelper.GenerateToken(user);
                LoginResponse loginResponse = new LoginResponse
                {
                    Token = token,
                    UserName = user.Name,
                    Role = user.Role
                };
                response = new CommonResponse
                {
                    StatusCode = 200,
                    Message = "Login succesfull",
                    Data = user
                };
                return Ok(response);
            }
            finally { }
        }
       
    }
}
