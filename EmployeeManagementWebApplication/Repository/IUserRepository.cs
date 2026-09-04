using EmployeeManagementWebApplication.Model;

namespace EmployeeManagementWebApplication.Interface
{
    public interface IUserRepository
    {
        Task<User> Login(LoginRequest request);
    }
}
