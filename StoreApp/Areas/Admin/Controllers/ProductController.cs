using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers{
    [Area("Admin")]

    public class ProductController:Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(){
            var model= _manager.ProductService.GetAllProducts(false);
            return View(model);            
        }
        
        public IActionResult Create(){
            ViewBag.Categories=
            new SelectList(_manager.CategoryService.GetCategories(false),"CategoryId","CategoryName","1");            
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Product product){

            if(ModelState.IsValid){
             _manager.ProductService.CreateProduct(product);
            return RedirectToAction("Index");
            }
            return View();
            
        }


        public IActionResult Update([FromRoute(Name ="id")] int id){

           var model = _manager.ProductService.GetOneProduct(id,false);
           return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] Product product){

            if(ModelState.IsValid){
                
            _manager.ProductService.UpdateOneProduct(product);
            return RedirectToAction("Index");
            }
            return View();
            
        }

       
        public IActionResult Delete([FromRoute(Name ="id")] int id){

            if(ModelState.IsValid){
                
            _manager.ProductService.DeleteOneProduct(id);
            return RedirectToAction("Index");
            }
            return View();
            
        }
 
      
    }
}