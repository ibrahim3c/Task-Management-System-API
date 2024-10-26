using Core.Constants;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Task_Management_System_API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
