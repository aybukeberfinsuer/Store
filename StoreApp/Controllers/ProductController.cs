using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using Entities.RequestParameters;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(_ProductRequestParameters param)
        {
            
            ViewData["Title"]="Products";
             var model = _manager.ProductService.GetAllProductsWithDetails(param);
             var pagination= new Pagination(){
                CurrenPage=param.PageNumber,
                ItemsPerPage=param.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
             };
            return View(new ProductlistViewModel(){
                Products=model,
                Pagination=pagination
            });
        }


        public IActionResult Get([FromRoute(Name = "id")] int id)
        {

            var model = _manager.ProductService.GetOneProduct(id, false);
            return View(model);
        }

    }
}