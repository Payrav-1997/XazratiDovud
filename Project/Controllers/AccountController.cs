using Castle.Core.Logging;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Account;
using Services.Account.Models;
using Services.Account.Repository;
using Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers   
{
    public class AccountController : Controller
    {
      //  private readonly ILogger _logger;
        private readonly UsersRepository _userRepository;
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public AccountController(UsersRepository userRepository,UserService userService,EmailService emailService)
        {
           // this._logger = logger;
            _userRepository = userRepository;
            this._userService = userService;
            this._emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userRepository.Create(
                    new User
                    {
                        UserName = model.Email,
                        Email = model.Email
                    }, model.Password);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }
          [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
            // new LoginViewModel() { Email = "Admin-1@mail.ru", Password = "Admin-123" }
        }


        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel model)
        {
            var response = await _userService.SignIn(model);
            if (!response.Succeeded)
            {
                ModelState.AddModelError("", "Пользователь не идентифицирован!");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _emailService.Send(email, "Восттановление пароля", "");
            return View("Index", "ForgotPasswordConfirm");
        }
    }
}
