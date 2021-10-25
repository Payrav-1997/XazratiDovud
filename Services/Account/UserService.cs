using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Account.Models;
using Services.Account.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account
{
    public class UserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UsersRepository _userRepository;

        public UserService(SignInManager<User> signInManager,UsersRepository userRepository)
        {
            this._signInManager = signInManager;
            this._userRepository = userRepository;
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

        public async Task<SignInResult>SignIn(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            return result;
        }


    }
}
