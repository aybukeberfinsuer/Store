using System.Diagnostics;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IServiceManager _servicemanager;

        public UserController(IServiceManager servicemanager)
        {
            _servicemanager = servicemanager;
        }

        public IActionResult Index()
        {
            return View(_servicemanager.AuthService.GetAllUser());
        }

        public IActionResult Create()
        {
            return View(new UserDtoForCreation()
            {
                Roles = new HashSet<string>(_servicemanager
                    .AuthService
                    .Roles
                    .Select(r => r.Name)
                    .ToList())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
        {
            var result = await _servicemanager.AuthService.CreateUser(userDto);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View();
        }

        public async Task<IActionResult> Update([FromRoute(Name="id")] string id) {

            var user = await _servicemanager.AuthService.GetOneUserForUpdate(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto){
            if(ModelState.IsValid){
                await _servicemanager.AuthService.Update(userDto);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ResetPassword([FromRoute(Name ="id")] string id){
            return View(new ResetPasswordDto(){
                UserName=id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model){

            var result= await _servicemanager.AuthService.ResetPassword(model);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View();
         }


         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOneUser([FromForm] UserDto userDto){

           var result= await _servicemanager.AuthService.DeleteOneUser(userDto.UserName);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View();
        }



    }
}