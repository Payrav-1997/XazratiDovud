using System.Threading.Tasks;
using Domain.Constats;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seed
{
    public static class DefaultRoleSeed
    {
        public  static async  Task AddDefaultRoleAsync(RoleManager<Roles> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Role.Admin))
                await roleManager.CreateAsync(new Roles {Name = Role.Admin});
            if (!await roleManager.RoleExistsAsync(Role.User))
                await roleManager.CreateAsync(new Roles {Name = Role.User});
        }
    }
}