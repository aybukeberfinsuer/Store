using Entities.Models;

namespace Services.Contracts{

    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(bool trackChanges);
    }
}