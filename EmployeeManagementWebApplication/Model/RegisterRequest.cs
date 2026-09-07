namespace EmployeeManagementWebApplication.Model
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public string Password { get; set; }
    }
}
