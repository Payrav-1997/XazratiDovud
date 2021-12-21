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
       
                   services.AddDbContext<DataContext>((builder) =>
                   {
                       builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies();
                   });
                   
                   services.Configure<SecurityStampValidatorOptions>(options =>
                   {
                       // enables immediate logout, after updating the user's stat.
                       options.ValidationInterval = TimeSpan.Zero;
                   });
       
                   return services;
        }
    }
}
