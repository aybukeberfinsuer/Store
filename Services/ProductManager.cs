using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services{

    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _repositorymanager;
        private readonly IMapper _mapper;



        public ProductManager(IRepositoryManager repositorymanager, IMapper mapper)
        {
            _repositorymanager = repositorymanager;
            _mapper = mapper;
        }

        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            Product product= _mapper.Map<Product>(productDto);
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