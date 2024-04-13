using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

                 options.EnableSensitiveDataLogging(true);
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services){

            services.AddIdentity<IdentityUser,IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount=false;
                options.User.RequireUniqueEmail=true;
                options.Password.RequireUppercase=false;
                options.Password.RequireLowercase=false;
                options.Password.RequireDigit=false;
                options.Password.RequiredLength=6;

            })
            .AddEntityFrameworkStores<RepositoryContext>();
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
            service.AddScoped<IAuthService,AuthManager>();
        }

        public static void ConfigureRouting(this IServiceCollection service){
            service.AddRouting(options => 
            {
                options.LowercaseUrls=true;
                options.AppendTrailingSlash=false;
            });
        }

        public static void ConfigureApplicationCookie(this IServiceCollection services){
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath= new PathString("/Account/Login");
                options.ReturnUrlParameter= CookieAuthenticationDefaults.ReturnUrlParameter;
                options.ExpireTimeSpan=TimeSpan.FromMinutes(10);
                options.AccessDeniedPath=new PathString("/Account/AccessDenied");

            });
        }


    }




}