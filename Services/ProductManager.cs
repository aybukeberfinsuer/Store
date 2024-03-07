using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services{

    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _repositorymanager;

        public ProductManager(IRepositoryManager repositorymanager)
        {
            _repositorymanager = repositorymanager;
        }

        public void CreateProduct(Product product)
        {
           _repositorymanager.Product.Create(product);
           _repositorymanager.Save();

        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _repositorymanager.Product.GetAllProducts(trackChanges);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product=_repositorymanager.Product.GetOneProduct(id,trackChanges);
            if(product is null)
                throw new Exception("Product not found!");
            return product;
            
        }
    }


}