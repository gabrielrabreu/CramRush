using Cramming.Account.Domain.Common;
using Cramming.Account.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cramming.Account.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }

    public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
                                                 IConfiguration configuration,
                                                 ApplicationDbContext context,
                                                 UserManager<ApplicationUser> userManager,
                                                 RoleManager<ApplicationRole> roleManager)
    {
        public async Task InitialiseAsync()
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task TrySeedAsync()
        {
            await EnsureRolesExistsAsync();
            await EnsureAdminUserExistsAsync();
        }

        private async Task EnsureRolesExistsAsync()
        {
            var roles = Roles.GetAllRoles();
            foreach (var role in roles)
                await EnsureRoleExistsAsync(role);
        }

        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new ApplicationRole(roleName);
                await roleManager.CreateAsync(role);
            }
        }

        private async Task EnsureAdminUserExistsAsync()
        {
            var adminUserName = configuration["AdminUser:UserName"]!;
            var adminEmail = configuration["AdminUser:Email"]!;
            var adminPassword = configuration["AdminUser:Password"]!;

            var administrator = new ApplicationUser { UserName = adminUserName, Email = adminEmail };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, adminPassword);
                await userManager.AddToRoleAsync(administrator, Roles.Administrator);
            }
        }
    }
}
