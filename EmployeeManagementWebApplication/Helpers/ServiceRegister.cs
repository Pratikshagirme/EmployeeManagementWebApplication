using EmployeeManagementWebApplication.Interface;
using EmployeeManagementWebApplication.Repository;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeManagementWebApplication.Helpers
{
    public static class ServiceRegister
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<DataBaseConnection>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
        
    }
}
