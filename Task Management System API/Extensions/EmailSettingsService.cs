using Task_Management_System_API.Helpers;

namespace Task_Management_System_API.Extensions
{
    public static class EmailSettingsService
    {
        public static IServiceCollection AddEmailSettingsService(this IServiceCollection services,IConfiguration configuration) {

            services.Configure<MaillingSettings>(configuration.GetSection("FakeMailSettings"));

            //sendGrid
            services.Configure<SendGridSettings>(configuration.GetSection("SendGridSettings"));

            return services;
        
        }
    }
}
