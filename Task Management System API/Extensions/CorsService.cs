namespace Task_Management_System_API.Extensions
{
    public static class CorsService
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services) {

            services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
