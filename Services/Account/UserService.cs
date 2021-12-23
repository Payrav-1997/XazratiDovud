using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Account.Models;
using Services.Account.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account
{
    public class UserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UsersRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(SignInManager<User> signInManager,UsersRepository userRepository,UserManager<User> userManager)
        {
            this._signInManager = signInManager;
            this._userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<User>Create(User model, string password)
        {
            await this.Validate(model);
            if (string.IsNullOrEmpty(model.UserName))
            {
                model.UserName = model.Email;
            }

            model.PhoneNumberConfirmed = true;
            model.Created = DateTime.UtcNow;
            model.Updated = DateTime.UtcNow;
            model.EmailConfirmed = true;
            model.Active = true;
            model.SecurityStamp = Guid.NewGuid().ToString("D");
            if (string.IsNullOrEmpty(model.Email))
            {
                model.Email = model.UserName + "@";
            }
            await _userRepository.Create(model, password);
            return model;
        }

        private async Task Validate(User model)
        {
            var queryable = _userRepository.Entities;
            var entity = await queryable.FirstOrDefaultAsync(m =>
            m.UserName.ToUpper() == model.UserName.ToUpper()
            && m.Id != model.Id);
            if (entity != null)
            {
                throw new Exception($"Пользователь с таким именим {entity.Email} уже существует!");
            }
        }

        public async Task<ClaimsPrincipal>SignIn(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;
            if (user.LockoutEnabled == false)
                return null;
            var result = await  this._signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (!result.Succeeded) return null;
            var clims = await _userManager.GetClaimsAsync(user);
           
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("MinExperience", 5.ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            userClaims.AddRange(clims.Select(x=>new Claim(ClaimTypes.Role,x.Value)));
            var claimsIdentity = new ClaimsIdentity("Claims");
            claimsIdentity.AddClaims(userClaims);

            var userPrincipal = new ClaimsPrincipal(claimsIdentity);

            return userPrincipal;
        }


    }
}
