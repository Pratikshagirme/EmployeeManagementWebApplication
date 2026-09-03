using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWebApplication.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeRequest
    {
        public int? Id { get; set; } = 0;

        [Required(ErrorMessage ="EmployeeName is required")]
        [StringLength(30,MinimumLength=2,ErrorMessage ="Employee Name must be between 2 and 30")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Email is must be vaild format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        [StringLength(30, MinimumLength =2, ErrorMessage = "Department name must be greater then two")]
        public string Department { get; set; } = string.Empty;


        [Required(ErrorMessage = "Salary is required")]
        [Range(4000,1000000,ErrorMessage="Salary is not valid")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "IsActive Bit is required")]
        public bool IsActive { get; set; }
    }

    public class EmployeeRespnse
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
    }

    
}
