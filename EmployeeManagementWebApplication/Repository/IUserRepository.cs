using EmployeeManagementWebApplication.Model;

namespace EmployeeManagementWebApplication.Interface
{
    public interface IUserRepository
    {
        Task<CommonResponse> Register(RegisterRequest request);
        Task<UserLoginData> LoginUser(LoginRequest request);
         Task<CommonResponse> GetUserProfile(int userId);
         Task<CommonResponse> UpdateUserProfile(int userId, string name, string email);
        Task<CommonResponse> LinkEmployeeToUser(int employeeId,int userId);
    }
}