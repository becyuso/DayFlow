using DayFlow.BuildingBlocks.DependencyInjection;
using DayFlow.Modules.Identity;
using DayFlow.Modules.Note;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

var dayflowDbConn = builder.Configuration.GetConnectionString("DayflowDb");

// Add services to the container.
var mvc = builder.Services
    .AddControllersWithViews()
    .AddViewLocalization(
        LanguageViewLocationExpanderFormat.Suffix
    ); ;
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/SignIn";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

// Add module
builder.Services
    .AddBuildingBlocks()
    .AddIdentityModule(builder.Configuration, dayflowDbConn)
    .AddNoteModule(builder.Configuration, dayflowDbConn); ;

mvc
    .AddIdentityPresentation()
    .AddNotePresentation();

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
