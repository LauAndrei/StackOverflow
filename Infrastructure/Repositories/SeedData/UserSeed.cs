using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.SeedData;

public static class UserSeed
{
    public static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User()
            {
                FirstName = "Test1",
                LastName = "User1",
                Email = "testuser@test.com",
                UserName = "FirstUser",
                
            };
            
            await userManager.CreateAsync(user, "Password1!");
            await userManager.AddToRoleAsync(user, ROLES_CONSTANTS.ROLES.STANDARD);
            
            var user2 = new User()
            {
                FirstName = "Test2",
                LastName = "User2",
                Email = "testuser2@test.com",
                UserName = "SecondUser",
                
            };

            await userManager.CreateAsync(user2, "Password1!");
            await userManager.AddToRoleAsync(user2, ROLES_CONSTANTS.ROLES.MODERATOR);
        }
    }
}