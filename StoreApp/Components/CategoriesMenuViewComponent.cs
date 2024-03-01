using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace StoreApp.Components{

    public class CategoriesMenuViewComponent:ViewComponent
    {
        private IServiceManager _servieManager;

        public CategoriesMenuViewComponent(IServiceManager servieManager)
        {
            _servieManager = servieManager;
        }

        public IViewComponentResult Invoke()
        {
            
                var categories = _servieManager.CategoryService.GetCategories(false);
                return View(categories);
            
        }
    }
}