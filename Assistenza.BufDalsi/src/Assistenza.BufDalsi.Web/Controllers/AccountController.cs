using Assistenza.BufDalsi.Web.Controllers;
using Assistenza.BufDalsi.Web.Data;
using Assistenza.BufDalsi.Web.Models;
using Assistenza.BufDalsi.Web.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace IdentitySampleApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, UserViewModel uModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(uModel.UserName);
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (user.UserName == model.UserName)
                    {
                        ApplicationRole applicationRole = await roleManager.FindByIdAsync(user.RoleId);
                        if (applicationRole != null)
                        {
                            if (applicationRole.Name == "Operator")
                            {
                                return RedirectToAction(nameof(HomeController.Operator), "Home/Index");
                            }
                            else if (applicationRole.Name == "User")
                            {
                                return RedirectToAction(nameof(ImpiantoController.Index), "Impianto/ViewImpiantiByClient");
                            }
                            else
                            {
                                return RedirectToAction(nameof(UserController.Index), "User/Index");
                            }
                        }
                    }
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username o password errati");
                    return View(model);
                    //return RedirectToAction(nameof(HomeController.Index), "Home/Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home/Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
