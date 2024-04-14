using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _servicemanager;

        public ProductsController(IServiceManager servicemanager)
        {
            _servicemanager = servicemanager;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_servicemanager.ProductService.GetAllProducts(false));
        }
    }
}