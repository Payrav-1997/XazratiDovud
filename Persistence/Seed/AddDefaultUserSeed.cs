using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seed
{
    public static class AddDefaultUserSeed
    {
        public static async Task AddDefaultUserAsync(UserManager<User> userManager)
        {
            if (await userManager.FindByEmailAsync("Admin") == null)
            {
                var user = new User {UserName = "Admin", Email = "Admin-1@mail.ru", LockoutEnabled = false};
                await userManager.CreateAsync(user, "Admin-123");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}