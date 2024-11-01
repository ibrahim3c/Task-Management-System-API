using Core.IRepositoreis.UOW;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DAL.Repositories.UOW;
using MyShop.Services.Implementations;
using MyShop.Services.Interfaces;
using Task_Management_System_API.Services.Implementations;
using Task_Management_System_API.Services.Interfaces;

namespace Task_Management_System_API.Extensions
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddDependencyInjectionService( this IServiceCollection services) {

            // Repository pattern with unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IProjectService,ProjectService>()
                .AddScoped<ITaskService, TaskService>()
                .AddScoped<IFileService, FileService>()
                .AddScoped<IAuthService, AuthService>()
                ;

           return services;
        }
    }
}
