﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Homework_22_Web.Models;
using Homework_22_Web.ViewModels;

namespace Homework_22_Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            string defaultRole = "user";

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };

                // create user
                var createResult = await _userManager.CreateAsync(user, model.Password);

                // set role to user
                var addToRole = await _userManager.AddToRoleAsync(user, defaultRole);


                if (createResult.Succeeded && addToRole.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}