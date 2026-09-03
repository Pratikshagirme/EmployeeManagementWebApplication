using EmployeeManagementWebApplication.Model;

namespace EmployeeManagementWebApplication.Interface
{
    public interface IEmployeeRepository
    {
        public Task<CommonResponse> GetAllEmployees();
        public Task<CommonResponse> GetEmployeeById(int Id);
        public Task<CommonResponse> InsertEmployee(Employee employee);
        public Task<CommonResponse> UpdateEmployee(Employee employee);
        public Task<CommonResponse> DeleteEmployee(int Id);
    }
}
