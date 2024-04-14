using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace  StoreApp.Components
{
    public class CategorySummary : ViewComponent
    {
        private readonly IServiceManager _manager;

        public CategorySummary(IServiceManager manager)
        {
            _manager = manager;
        }

        public string Invoke(){
            return _manager.CategoryService
            .GetCategories(false)
            .Count()
            .ToString();
        }
    }
    
}