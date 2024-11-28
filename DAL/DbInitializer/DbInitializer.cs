using Core.Constants;
using Core.Models;
using DAL.Data;
using DAL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DAL.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext appDbContext;
        private readonly IOptionsMonitor<AdminUser> adminUserConfigs;

        public DbInitializer(UserManager<AppUser> userManager, AppDbContext appDbContext,IOptionsMonitor<AdminUser> AdminUserConfigs)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
            adminUserConfigs = AdminUserConfigs;
        }
        public async void Initialize()
        {

            if (appDbContext.Database.GetMigrations().Any())
            {
                // Apply migrations
                appDbContext.Database.Migrate();

            }


            
            // user
            var createUserResult = userManager.CreateAsync(new AppUser
            {
                Email = adminUserConfigs.CurrentValue.Email,
                Address = adminUserConfigs.CurrentValue.Address,
                PhoneNumber = adminUserConfigs.CurrentValue.PhoneNumber,
                UserName =adminUserConfigs.CurrentValue.UserName
             
            }, adminUserConfigs.CurrentValue.Password).GetAwaiter().GetResult();



            var existingUser = appDbContext.Users.FirstOrDefault(u => u.Email == adminUserConfigs.CurrentValue.Email);


            // Add user to role if not already assigned
            if (!userManager.IsInRoleAsync(existingUser, Roles.AdminRole).Result)
            {
                userManager.AddToRoleAsync(existingUser, Roles.AdminRole).GetAwaiter().GetResult();
            }

            if (!userManager.IsInRoleAsync(existingUser, Roles.UserRole).Result)
            {
                userManager.AddToRoleAsync(existingUser, Roles.UserRole).GetAwaiter().GetResult();
            }

        }

    }
}

