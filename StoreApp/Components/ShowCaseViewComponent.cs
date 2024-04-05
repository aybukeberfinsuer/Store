using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ShowCaseViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public ShowCaseViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }


        public IViewComponentResult Invoke(){
            var products = _manager.ProductService.GetShowCaseProducts(false);
            return View(products);
        }
    }
    
}