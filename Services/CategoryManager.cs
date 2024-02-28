using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services{

    public class CategoryManager : ICategoryService
    {
        private IRepositoryManager _repositroymanager;

        public CategoryManager(IRepositoryManager repositroymanager)
        {
            _repositroymanager = repositroymanager;
        }

        public IEnumerable<Category> GetCategories(bool trackChanges)
        {
            return _repositroymanager.Category.FindAll(trackChanges);
        }
    }
}