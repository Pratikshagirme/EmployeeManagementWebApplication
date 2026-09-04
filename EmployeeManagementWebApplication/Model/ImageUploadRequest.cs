namespace EmployeeManagementWebApplication.Model
{
    public class ImageUploadRequest
    {
        public int EmployeeId { get; set; }

        public IFormFile Image { get; set; }
    }
}
