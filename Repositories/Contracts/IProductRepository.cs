using Entities.Models;
using Entities.RequestParameters;

namespace Repositories.Contracts{
   public interface IProductRepository:IRepositoryBase<Product>
{
    IQueryable<Product> GetAllProducts(bool trackChanges);
    IQueryable<Product> GetAllProductsWithDetails(_ProductRequestParameters param);
    IQueryable<Product> GetShowCaseProducts(bool trackChanges);
    Product? GetOneProduct(int id, bool trackChanges);
    void CreateOneProduct(Product product);
     void DeleteOneProduct(Product product);
     void UpdateOneProduct(Product entity);
     
    }

}