using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Task_Management_System_API.Extensions
{
    public static  class JWTConfigurationService
    {
        public static IServiceCollection AddJWTConfigurationService(this IServiceCollection services,IConfiguration configuration)
        {

            // to use jwt token to check authantication =>[authorize]

            services.AddAuthentication(options => {
                // to change default authantication to jwt 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                //  if u are unauthanticated it will redirect you to login form
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                // if there other schemas make is default of jwt
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


                // these configs to check if has token only but i want to check if he has right claims
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                // check if token have specific data
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),

                    // if u want when the token expire he does not give me مهله بعض الوقت 
                    ClockSkew = TimeSpan.Zero

                };
            }

                );


            return services; 
        }
    }
}
