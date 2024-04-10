using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signinmanager;

        public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _usermanager.FindByNameAsync(model.UserName);
                if (user is not null)
                {
                    await _signinmanager.SignOutAsync();
                    if ((await _signinmanager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return Redirect(model?.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("Error", "Invalid username or password.");
            }
            return View();
        }

        public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            await _signinmanager.SignOutAsync();
            return Redirect(ReturnUrl);
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _usermanager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleResult = await _usermanager
                .AddToRoleAsync(user, "User");

                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Login", new {ReturnUrl="/"} );
                }
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);

                }
            }
            return View();
        }
    }

}