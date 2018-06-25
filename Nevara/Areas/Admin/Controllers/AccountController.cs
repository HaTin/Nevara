using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nevara.Areas.Admin.Models;
using Nevara.Extensions;
using Nevara.Models.Entities;

namespace Nevara.Areas.Admin.Controllers
{
    [Authorize]
    [Route("[area]/[controller]/[action]")]
    [Area("Admin")]  
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(
             UserManager<AppUser> userManager,
             SignInManager<AppUser> signInManager
             )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            /*if ( _signInManager.IsSignedIn(User))
            {
                return RedirectToLocal("/admin/home");
            }*/
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                   return RedirectToLocal(returnUrl);
                }              
            
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect UserName or Password");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }       
                return Redirect("/Admin/Home");
            
        }
        [AllowAnonymous]
        [HttpGet]   
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Admin/Account/Login");
        }
    }
   
}