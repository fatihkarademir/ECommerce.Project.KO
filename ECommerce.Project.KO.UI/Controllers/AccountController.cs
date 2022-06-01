using ECommerce.Project.KO.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //send mail
                await _userManager.AddToRoleAsync(user, "Customer");
                return RedirectToAction("account", "login");
            }

            ViewBag.ErrorMessage = result.Errors.Select(i => i.Description).ToList();

            return View(model);
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }

        //Login URL post'a taşıyacağız
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //returnUrl = returnUrl ?? "~/";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.EMail);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu mail ile daha önce hesap oluşturulmamış");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                //return RedirectToAction("Home", "Index");
                return Redirect(model.ReturnUrl ?? "~/");
            }
            ModelState.AddModelError("", "Mail veya şifre yanlış");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}
