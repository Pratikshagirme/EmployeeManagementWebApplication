using EmployeeManagementWebApplication.Model;

namespace EmployeeManagementWebApplication.Interface
{
    public interface IBulkEmployeeRepository
    {
        public Task BulkInsert(List<Employee> employees);
       
    }
}
