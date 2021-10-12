﻿using Core.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ProjectService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserClaimsPrincipalFactory<User>, ClaimsPrincipalFactory>();
            services.AddSingleton<ExceptionLogingAttribute>();

            return services;
        }
    }
}
