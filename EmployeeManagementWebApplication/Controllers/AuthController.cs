using EmployeeManagementWebApplication.Helpers;
using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login(LoginRequest request)
        {
            CommonResponse response = new CommonResponse();

            UserLoginData user = await _repository.LoginUser(request);

            if (user == null)
            {
                response = new CommonResponse
                {
                    StatusCode = 401,
                    Message = "Invalid email or password",
                    Data = null
                };

                return Ok(response);
            }

            
            string token = _jwtTokenHelper.GenerateToken(user);

            LoginResponse loginResponse = new LoginResponse
            {
                Token = token,
                UserName = user.Name,
                Role = user.RoleName
            };

            response = new CommonResponse
            {
                StatusCode = 200,
                Message = "Login successful",
                Data = loginResponse
            };

            return Ok(response);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            CommonResponse response =
                await _repository.Register(request);

            return Ok(response);
        }
    }
}