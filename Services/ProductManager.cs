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

        public void DeleteOneProduct(int id)
        {
            Product product =GetOneProduct(id,false);
            if(product is not null){

               _repositorymanager.Product.DeleteOneProduct(product); 
               _repositorymanager.Save();
            }
                      
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _repositorymanager.Product.GetAllProducts(trackChanges);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product=_repositorymanager.Product.GetOneProduct(id,trackChanges);
            if(product is null)
                throw new Exception("Product not found with ID: " + id);
            return product;
            
        }

        public void UpdateOneProduct(Product product)
        {
            var model =_repositorymanager.Product.GetOneProduct(product.Id,true);
            model.ProductName=product.ProductName;
            model.Price=product.Price;
            _repositorymanager.Save();
        }
    }


}