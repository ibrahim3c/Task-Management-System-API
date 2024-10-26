using System.Reflection;

namespace Task_Management_System_API.Extensions
{
    public static class AutoMapperService
    {
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            return services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
