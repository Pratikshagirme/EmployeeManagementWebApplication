using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagementWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;

        }
        [Authorize(Roles ="User")]
        [HttpGet("MyProfile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            int id = Convert.ToInt32(userId);
            CommonResponse response = await _repository.GetUserProfile(id);


            return Ok(response);

        }
        [Authorize(Roles = "User")]
        [HttpPut("MyProfile")]
        public async Task<IActionResult> UpdateMyProfile(UpdateUserProfileRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            int id = Convert.ToInt32(userId);

            CommonResponse response =await _repository.UpdateUserProfile(id,request.Name,request.Email);
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("LinkEmployee")]
        public async Task<IActionResult> LinkEmployeeToUser(LinkEmployeeRequest request)
        {
            CommonResponse response =await _repository.LinkEmployeeToUser(request.EmployeeId,request.UserId);

            return Ok(response);
        }
    }
}
