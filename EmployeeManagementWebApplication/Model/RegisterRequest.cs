using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWebApplication.Model
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2,
           ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be in valid format")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; } = string.Empty;
    }
}
