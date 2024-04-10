using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers{

    [Area("Admin")]
    public class UserController:Controller 
    {
        private readonly IServiceManager _servicemanager;

        public UserController(IServiceManager servicemanager)
        {
            _servicemanager = servicemanager;
        }

        public IActionResult Index(){
            return View(_servicemanager.AuthService.GetAllUser());
        }
    }
}