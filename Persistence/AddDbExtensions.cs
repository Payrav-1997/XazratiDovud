using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class AddDbExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider()
                .GetRequiredService<IConfiguration>();

            var connection = new Npgsql.NpgsqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"))
            {
                Timeout = 600,
                MaxPoolSize = 5000,
                MinPoolSize = 5
            };

            services.AddDbContext<DataContext>((builder) =>
            {
                builder.UseLazyLoadingProxies();
                builder.UseNpgsql(connection.ToString(), sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("Persistence");
                    //sqlServerOptions.CommandTimeout(600);
                    sqlServerOptions.EnableRetryOnFailure();
                });
            }, ServiceLifetime.Scoped);

            services.AddIdentity<User, Roles>(options => { options.User.AllowedUserNameCharacters = null; })
                .AddRoleManager<RoleManager<Roles>>()
                .AddUserManager<UserManager<User>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            });

            return services;
        }
    }
}
