using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Homework_22.ViewModels;
using Homework_22.Models;

namespace Homework_22.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl) && !string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction(nameof(Index), "Home");
                }
            }

            ModelState.AddModelError(String.Empty, "Wrong user or password");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            string defaultRole = "user";

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };

                // add user
                var createResult = await _userManager.CreateAsync(user, model.Password);

                // set role to user
                var addToRole = await _userManager.AddToRoleAsync(user, defaultRole);

                if (createResult.Succeeded && addToRole.Succeeded)
                {
                    // set cookies
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(Index), "Home");
                }

                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError(String.Empty, identityError.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // remove cookies
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
