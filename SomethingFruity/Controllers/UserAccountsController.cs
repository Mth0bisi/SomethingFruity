using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomethingFruity.Models.ViewModels;
using SomethingFruity.Models;
using Microsoft.AspNetCore.Identity;
using SomethingFruity.Data;
using SomethingFruity.Security;
using Serilog;

namespace SomethingFruity.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly SomethingFruityDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISignInManager _signInManagerV2;

        public UserAccountsController(SomethingFruityDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, ISignInManager signInManagerV2)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _signInManagerV2 = signInManagerV2;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    TempData["Error"] = "Email not found. Please try again";
                    Log.Information($"User {model.Email} tried to access the system.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    await _signInManagerV2.SignInAsync(model.Email.ToUpper());
                    Log.Information($"User {User.Identity.Name} has successfully logged in to the system.");
                    return RedirectToAction("Index", "Home");
                }
                // return Redirect(returnUrl);
                else
                {
                    TempData["Error"] = "Wrong credentials. Please try again";
                    Log.Information($"User {User.Identity.Name} has entered wrong credentials.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.MessageError = "Incorrect Username or Password. Please try again.";

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                TempData["Error"] = "This email address is already in use.";
                Log.Information($"User {User.Identity.Name} has failed to register into the system.");
                return View(model);
            }

            var user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    TempData["Error"] = error.Description;
                }

                return View(model);
            }

            // await _userManager.AddToRoleAsync(user, "User");
            TempData["Success"] = "User successfully created.";
            Log.Information($"User {User.Identity.Name} has been successfully registered.");
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await _signInManagerV2.SignOutAsync();

            var basePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var returnUrl = basePath + "/Home/Index";
            Log.Information($"User {User.Identity.Name} has logged off.");

            return Redirect(returnUrl);
        }
    }

}
