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

    }

}