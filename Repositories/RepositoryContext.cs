using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories{
public class RepositoryContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories {get; set;}

        public RepositoryContext(DbContextOptions<RepositoryContext> options) :base(options)
        {
        
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product(){Id=1, ProductName="Computer", Price=17_000},
                new Product(){Id=2, ProductName="Keyboard", Price=1_000},
                new Product(){Id=3, ProductName="Mouse", Price=500}
                
            );

            modelBuilder.Entity<Category>().HasData(
                new Category(){CategoryId=1, CategoryName="Book"},
                new Category(){CategoryId=2, CategoryName="Electronic"}
            );
        }

    }
}
 