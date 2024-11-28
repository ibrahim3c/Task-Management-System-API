using Asp.Versioning;

namespace Task_Management_System_API.Extensions
{
    public static class ApiVersioningConfigs
    {
        public static IServiceCollection AddApiVersioningConfigs(this IServiceCollection services) {
            services.AddApiVersioning(options =>
            {
                // if client does not add version => use default
                options.AssumeDefaultVersionWhenUnspecified = true;
                // the default=> 1.0
                options.DefaultApiVersion = new ApiVersion(1, 0);
                // show the version 
                options.ReportApiVersions = true;
                // ways to expose version in api
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-version"),
                    new UrlSegmentApiVersionReader()
                    ,new MediaTypeApiVersionReader("ver")
                );
                // for swagger
            }).AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl= true;
            });
            return services;
        }
    }
}
