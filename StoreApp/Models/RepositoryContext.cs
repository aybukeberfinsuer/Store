using Microsoft.EntityFrameworkCore;

namespace StoreApp.Models
{
    public class RepositoryContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
    
    public RepositoryContext(DbContextOptions<RepositoryContext> options) :base(options)
    {
        
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product(){Id=1, PrdoductName="Computer", Price=17_000},
                new Product(){Id=2, PrdoductName="Keyboard", Price=1_000},
                new Product(){Id=3, PrdoductName="Mouse", Price=500}
                
            );
        }

    }
}