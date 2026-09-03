namespace EmployeeManagementWebApplication.Model
{
    public class CommonResponse
    {
       public int StatusCode { get; set; }
       public string Message { get; set; }
       public dynamic Data { get; set; }

    }
}
