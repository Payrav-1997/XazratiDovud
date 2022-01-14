using Core.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Services.Account;
using Services.Account.Repository;
using Services.File.Repository;
using Services.Hostory;
using Services.Hostory.Repository;
using Services.Mail;
using Services.Mapping;
namespace Services
{
    public static class ProjectService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            
            services.AddScoped<IUserClaimsPrincipalFactory<User>, ClaimsPrincipalFactory>();
            services.AddSingleton<ExceptionLogingAttribute>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<EmailService>();
            services.AddScoped<IHistoryService,HistoryService>();
            services.AddScoped<HistoryRepository>();
            services.AddScoped<FileRepository>();

            return services;
        }
    }
}
