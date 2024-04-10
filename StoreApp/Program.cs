using System.Net;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;
using StoreApp.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureSession();
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistiration();
builder.Services.ConfigureRouting();




builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoint =>
{
    endpoint.MapAreaControllerRoute(
        name:"Admin",
        areaName:"Admin",
        pattern:"Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoint.MapControllerRoute(
         "default",
         "{controller=Home}/{action=Index}/{id?}"
    );

    endpoint.MapRazorPages();
});

app.ConfigureAndCheckMigration();
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();
app.Run();
