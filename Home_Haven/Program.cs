using Home_Haven.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var ConnectionString = builder.Configuration.GetConnectionString("HomeHavenConnectionString");
builder.Services.AddDbContext<Home_Haven_DBContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

// Add sessions services to the container
builder.Services.AddSession(options =>
{
    // Customize session options as needed
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Adjust session timeout as needed
    options.Cookie.HttpOnly = true; // Secure session cookie
    options.Cookie.IsEssential = true; // Indicates that the cookie is essential for the app to work
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Set the culture to "en-US" for the application
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.Run();
