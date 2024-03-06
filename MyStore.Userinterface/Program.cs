using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyStore.Database;
using MyStore.Database.Interfaces;
using MyStore.Service.Services.Category.Implementation;
using MyStore.Service.Services.File;
using MyStore.Service.Services.Product;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// For Entity Framework
var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSqlServer")));

// Add services to the container.
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Categories/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Products/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddHttpClient("AuthenticationAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7045/api/Authentication/");
    client.Timeout = TimeSpan.FromSeconds(60);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();
