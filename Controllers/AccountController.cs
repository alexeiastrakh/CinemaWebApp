﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CinemaWebApplication.Models;
using CinemaWebApplication.ViewModels;
using CinemaWebApplication;
using Microsoft.AspNetCore.Authorization;

namespace CinemaWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityContext _context;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IdentityContext identityContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = identityContext;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                // додаємо користувача
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRolesAsync(user, new List<string> { "user" });
                if (result.Succeeded)
                {
                    // установка кукі
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
             return View(new LoginViewModel { ReturnUrl = returnUrl });
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var users = (from u in _context.Users where u.UserName == model.Email || u.Email == model.Email select u.UserName).ToList();
                string name = users.Any() ? users.First().ToString() : "";
                var result =
                    await _signInManager.PasswordSignInAsync(name, model.Password, model.RememberMe, false);
                    //model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // перевіряємо, чи належить URL додатку
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний логін чи (та) пароль");
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(User model)
        {
            if (ModelState.IsValid)
            {
                await _signInManager.SignInAsync(model, true);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // видаляємо аутентифікаційні куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


    }
}
