using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWebApplication.Model
{
    public class UpdateUserProfileRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be in valid format")]
        public string Email { get; set; } = string.Empty;
    }
}