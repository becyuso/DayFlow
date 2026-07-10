using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using DayFlow.Modules.Identity;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

var modules = new IModule[]
{
    new IdentityModule()
};

var dayflowDbConn = builder.Configuration.GetConnectionString("DayflowDb");

foreach (var module in modules)
{
    module.Register(builder.Services, builder.Configuration, dayflowDbConn);
}

// Add services to the container.
var mvc = builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/SignIn";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

// Register Identity module services (controllers as application parts)
foreach (var module in modules)
{
    module.RegisterMvc(mvc);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Ensure authentication middleware is in pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
