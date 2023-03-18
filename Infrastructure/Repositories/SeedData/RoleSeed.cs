using Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.SeedData;

public static class RoleSeed
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            var standardRole = new IdentityRole<int>(ROLES_CONSTANTS.ROLES.STANDARD);
            var moderatorRole = new IdentityRole<int>(ROLES_CONSTANTS.ROLES.MODERATOR);

            await roleManager.CreateAsync(standardRole);
            await roleManager.CreateAsync(moderatorRole);
        }
    }
}