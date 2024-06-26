using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace StoreApp.Controllers{

public class CategoryController:Controller
{
    private IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(){
            
            ViewData["Title"]="Categories";
            var model= _manager.CategoryService.GetCategories(false);
            return View(model);
        }
    }
}