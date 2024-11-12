using Task_Management_System_API.Helpers;

namespace Task_Management_System_API.Extensions
{
    public static class TwilioService
    {
        public static IServiceCollection AddTwilioService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<TwilioConfigs>(configuration.GetSection("Twilio"));

            return services;
        }
    }
}
