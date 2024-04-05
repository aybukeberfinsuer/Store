using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructure.Extensions
{

    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("sqlconnection"),
                 b => b.MigrationsAssembly("StoreApp"));
            });
        }

        public static void ConfigureSession(this IServiceCollection service)
        {

            service.AddDistributedMemoryCache();
            service.AddSession(options =>
            {
                options.Cookie.Name = "StoreApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            service.AddScoped<Cart>(c=>SessionCart.GetCart(c));

        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection service)
        {
            service.AddScoped<IRepositoryManager, RepositoryManager>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void ConfigureServiceRegistiration(this IServiceCollection service)
        {
            //Servis kısmı için
            service.AddScoped<IServiceManager, ServiceManager>();
            service.AddScoped<IProductService, ProductManager>();
            service.AddScoped<ICategoryService, CategoryManager>();
            service.AddScoped<IOrderService, OrderManager>();
        }


    }




}