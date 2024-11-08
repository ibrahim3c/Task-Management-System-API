using Core.Constants;
using Core.Models;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_Management_System_API.Extensions;
using Task_Management_System_API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region myConfigs
// connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No ConnectionString was found");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

//dependency Injection
builder.Services.AddDependencyInjectionService();

// cors policy
builder.Services.AddCorsService();

// automapper
builder.Services.AddAutoMapperService();

// identity ===> i spend one day to find out that you are the problem => it should be above JWTConfigs :(
builder.Services.AddIdentity<AppUser, AppRole>(
    options=>options.SignIn.RequireConfirmedEmail=true
    ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// JWTconfiguration
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddJWTConfigurationService(builder.Configuration);


// mailSettings
builder.Services.AddEmailSettingsService(builder.Configuration);



#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors(GeneralConsts.CorsPolicyName);
app.UseAuthentication(); // Ensures the user is authenticated
app.UseAuthorization(); // Then applies authorization policies


app.MapControllers();

app.Run();
