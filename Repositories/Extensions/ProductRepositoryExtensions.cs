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
    }    
}
//Extensionlarda ilk parametere önemli olmuyor, o hangi yapıyı extend edeceğimizi gösteriyor.