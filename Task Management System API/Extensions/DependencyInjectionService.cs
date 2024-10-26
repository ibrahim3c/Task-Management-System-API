using Core.IRepositoreis.UOW;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DAL.Repositories.UOW;

namespace Task_Management_System_API.Extensions
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddDependencyInjectionService( this IServiceCollection services) {

            // Repository pattern with unit of work
            services.AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IProjectService,ProjectService>()
                .AddTransient<ITaskService, TaskService>()
                ;

           return services;
        }
    }
}
