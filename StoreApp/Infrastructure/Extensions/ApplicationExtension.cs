using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        // Auto Migrate işlemi (dotnet ef databse update) için ihtiyaç duyduğunda otomatik update işlemi uygular
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {

            RepositoryContext context = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RepositoryContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

        }
        public static void ConfigureLocalization(this IApplicationBuilder app)
        {

            app.UseRequestLocalization(options => 
            {
                options.AddSupportedCultures("tr-TR","eng-US")
                .AddSupportedUICultures("tr-TR","eng-US")
                .SetDefaultCulture("tr-TR");
            });
        }

        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app){
           
            const string adminUser= "Admin";
            const string adminPassword= "123456";

            //UserManager
            UserManager<IdentityUser> userManager = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();

            //RoleManager
            RoleManager<IdentityRole> roleManager = app
            .ApplicationServices
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if(user is null)
            {
                user = new IdentityUser(){
                    Email="asuer@samsun.edu.tr",
                    UserName= adminUser,
                    EmailConfirmed=true
                };

                var result = await userManager.CreateAsync(user,adminPassword);
                if(!result.Succeeded)
                    throw new Exception("Admin user could not been created.");

                var roleResult = await userManager.AddToRolesAsync(user,
                roleManager
                .Roles
                .Select(r => r.Name)
                .ToList()         
                );

                if (!roleResult.Succeeded)
                    throw new Exception("System have problem with role defination for admin.");

            }
        }

    }

}