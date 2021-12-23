using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Threading.Tasks;

namespace Persistence
{
    public static class UseDbExtension
    {
        public static IApplicationBuilder  UseDb(this IApplicationBuilder builder)
        {
            using (var context = new DataContextFactory().CreateDbContext(new string[] { }))
            {
                context.Database.Migrate();
            }

            return builder;
        }


        public static async Task InitRoot(UserManager<User> userManager, RoleManager<Roles> roleManager, DataContext context)
        {

        }
    }
}
