using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public ClaimsPrincipalFactory(UserManager<User> userManager, 
            IOptions<IdentityOptions> optionsAccessor) 
            : base(userManager, optionsAccessor)
        {
        }
    }
}
