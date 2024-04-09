using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class AccountController:Controller 
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signinmanager;

        public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
        }

        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = await _usermanager.FindByNameAsync(model.UserName);
                if(user is not null)
                {
                    await _signinmanager.SignOutAsync();
                    if((await _signinmanager.PasswordSignInAsync(user, model.Password,false,false)).Succeeded)
                    {
                        return Redirect(model?.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("Error","Invalid username or password.");
            }
            return View();
        }
        
    }
    
}