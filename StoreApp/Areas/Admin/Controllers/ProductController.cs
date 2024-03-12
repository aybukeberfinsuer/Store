using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var model = _manager.ProductService.GetAllProducts(false);
            return View(model);
        }
        private SelectList GetCategoriesSelectlist()
        {
            return new SelectList(_manager.CategoryService.GetCategories(false), "CategoryId", "CategoryName", "1");
        }

        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoriesSelectlist();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto,IFormFile file)
        {

            if (ModelState.IsValid)
            {
                //file operation
                string path= Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images",file.FileName);
               using (var stream = new FileStream(path,FileMode.Create))
               {
                    await file.CopyToAsync(stream);
               }
               
               productDto.ImageUrl=String.Concat("/images/",file.FileName);
                _manager.ProductService.CreateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();

        }


        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories = GetCategoriesSelectlist();
            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto,IFormFile file)
        {

            if (ModelState.IsValid)
            {
                //file operation
                string path= Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images",file.FileName);
               using (var stream = new FileStream(path,FileMode.Create))
               {
                    await file.CopyToAsync(stream);
               }
               
               productDto.ImageUrl=String.Concat("/images/",file.FileName);

                _manager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();

        }


        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {

            if (ModelState.IsValid)
            {

                _manager.ProductService.DeleteOneProduct(id);
                return RedirectToAction("Index");
            }
            return View();

        }



    }
}