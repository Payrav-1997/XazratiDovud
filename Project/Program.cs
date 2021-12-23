using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Seed;

namespace Project
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            await context.Database.MigrateAsync();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Roles>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            await DefaultRoleSeed.AddDefaultRoleAsync(roleManager);
            await AddDefaultUserSeed.AddDefaultUserAsync(userManager);
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
