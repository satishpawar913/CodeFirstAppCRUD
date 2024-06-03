using CodeFirstApproachCRUD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApproachCRUD.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.Remove("IsLoggedIn");

            Response.Headers["Cache-Control"] = "no-cache, no-store";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";

            return RedirectToAction("Index", "Home");
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
                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign a role to the user
                    await userManager.AddToRoleAsync(user, "User");

                    await signInManager.SignInAsync(user, isPersistent: false);
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /* [HttpPost]
         public async Task<IActionResult> Login(LoginViewModel model)
         {
             if (ModelState.IsValid)
             {
                 var user = await userManager.FindByEmailAsync(model.Email);
                 if (user != null)
                 {
                     var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                     if (result.Succeeded)
                     {
                         HttpContext.Session.SetString("IsLoggedIn", "true");
                         return RedirectToAction("Index", "Home");
                     }
                 }

                 ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
             }
             return View(model);
         }*/

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("IsLoggedIn", "true");
                        var roles = await userManager.GetRolesAsync(user);

                        if (roles.Contains("AdminRole"))
                        {
                            return RedirectToAction("Index", "Department");
                        }
                        else if (roles.Contains("UserRole"))
                        {
                            return RedirectToAction("Index", "Employee");
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

    }
}
