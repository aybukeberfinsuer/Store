using Entities.Models;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtensions
    {
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products,int? categoryId ){

            if(categoryId is null)
                return products;
            else
                return products.Where(p => p.CategoryId.Equals(categoryId));
        }

        public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, String? searchTerm){
            if(string.IsNullOrWhiteSpace(searchTerm))
            return products;

            else
           return  products.Where(prd => prd.ProductName.ToLower().Contains(searchTerm.ToLower()));
        }

        public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products,int minprice,int maxprice,bool IsValidPrice){

            if(IsValidPrice)
                return products.Where(prd => prd.Price>=minprice && prd.Price<=maxprice);
            else
            return products;
        }
    }    
}
//Extensionlarda ilk parametere önemli olmuyor, o hangi yapıyı extend edeceğimizi gösteriyor.