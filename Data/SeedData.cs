using finalTicket.Models;
using Microsoft.AspNetCore.Identity;

namespace finalTicket.Data;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create an admin user
        var adminUser = new ApplicationUser
        {
            UserName = "admin"
        };

        string adminPassword = "Admin@123";
        var user = await userManager.FindByNameAsync("admin");

        if (user == null)
        {
            var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
            if (createPowerUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}