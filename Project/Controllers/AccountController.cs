using Castle.Core.Logging;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Account;
using Services.Account.Models;
using Services.Account.Repository;
using Services.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace Project.Controllers   
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UsersRepository _userRepository;
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public AccountController(UsersRepository userRepository,UserService userService,EmailService emailService,ILogger<AccountController> logger)
        {
            this._logger = logger;
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

                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userRepository.Create(user, model.Password);
                if(result.Succeeded)
                {

                    await _userRepository.AddToRoleAsync(user, "user");
                    return RedirectToAction("Login");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Code == "PasswordRequiresLower" ? "Пароль должен содержать символ в нижнем регистре" : error.Code == "Passwords must have at least one non alphanumeric character." ? "Пароль должен содержать один символ" : error.Code == "Passwords must have at least one digit('0' - '9')"? "Пароли должны содержать как минимум одну цифру (« 0 »-« 9 »)" : error.Code == "PasswordRequiresNonAlphanumeric" ? "Пароль должен быть буквенно-цифровым":"");
                }

                return View(model);
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
            return View(new LoginViewModel() { Email = "Admin-1@mail.ru", Password = "Admin-123" });
        }


        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel model)
        {
            var response = await _userService.SignIn(model);
            if (response ==null)
            {
                ModelState.AddModelError("", "Пользователь не идентифицирован!");
                return View(model);
            }

            await HttpContext.SignInAsync(
                response, new AuthenticationProperties() { IsPersistent = false, AllowRefresh = false });
            return Redirect("/Admin/Home/Privacy");
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
