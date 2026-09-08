using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWebApplication.Model
{
    public class LinkEmployeeRequest
    {
        [Required(ErrorMessage = "EmployeeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "EmployeeId must be greater than 0")]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "UserId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
        public int UserId { get; set; }
    }
}
