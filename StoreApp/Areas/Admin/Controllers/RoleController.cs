using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers{

    [Area("Admin")]    
    [Authorize(Roles ="Admin")]
    public class RoleController:Controller 
    {
        private readonly IServiceManager _servicemanager;

        public RoleController(IServiceManager servicemanager)
        {
            _servicemanager = servicemanager;
        }

        public IActionResult Index(){

            return View(_servicemanager.AuthService.Roles);
        }
    }
}