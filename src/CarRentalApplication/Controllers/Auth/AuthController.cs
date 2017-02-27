using AutoMapper;
using CarRentalApplication.Models;
using CarRentalApplication.Models.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Controllers.Auth
{
    public class AuthController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;

        public AuthController(SignInManager<AppUser> signInMgr, UserManager<AppUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _signInManager = signInMgr;
            _userManager = userMgr;
            _roleManager = roleMgr;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Dashboard");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userSignedIn = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false,false);
                if (userSignedIn.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Home", "Dashboard");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "UserName/Password is Invalid");
                }
            }
            return View(lvm);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(rvm.Email) == null)
                {
                    var newUser = Mapper.Map<AppUser>(rvm);
                    try
                    {
                        var userCreation = await _userManager.CreateAsync(newUser, rvm.Password);
                        if (userCreation.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, "customer");
                            await _signInManager.SignInAsync(newUser, false);
                            return RedirectToAction("Home", "Dashboard");
                        }
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "User Could No be Created");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This Account already Exists");
                }
            }
            return View(rvm);
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
