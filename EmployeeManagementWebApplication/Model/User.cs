using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWebApplication.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="name lengthi is between 2 and 50")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage ="Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&]).{8,20}$", ErrorMessage = "Password must be 8-20 characters and contain uppercase, lowercase, number and special character")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage ="role is required")]
        [RegularExpression("^(Admin|User)$",ErrorMessage ="role must be admin and user")]
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
