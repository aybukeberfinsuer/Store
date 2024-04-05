using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;

namespace Services.Contracts{

    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(bool trackChanges);
        IEnumerable<Product> GetShowCaseProducts(bool trackChanges);
        IEnumerable<Product> GetAllProductsWithDetails(_ProductRequestParameters param);
        Product? GetOneProduct(int id,bool trackChanges);
        void CreateProduct(ProductDtoForInsertion productDto);
        void UpdateOneProduct(ProductDtoForUpdate productDto);
        void DeleteOneProduct(int id);
        ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges);
    }
}