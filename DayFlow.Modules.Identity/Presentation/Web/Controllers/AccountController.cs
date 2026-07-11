using DayFlow.Modules.Identity.Application.Features.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DayFlow.Modules.Identity.Presentation.Web.Controllers
{
    /// <summary>
    ///   ADD  <Project Sdk="Microsoft.NET.Sdk"> => <Project Sdk="Microsoft.NET.Sdk.Razor">
    ///   ADD  <ItemGroup>
    ///            <FrameworkReference Include = "Microsoft.AspNetCore.App" />
    ///        </ItemGroup>
    ///   ADD套件 Microsoft.AspNetCore.Mvc
    ///   ADD  <PropertyGroup>
    ///            <AddRazorSupportForMvc>true</AddRazorSupportForMvc>
    ///        </PropertyGroup>
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password, string returnUrl = "/")
        {
            var result = await _mediator.Send(new Command(email, password));
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Invalid credentials");
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Email ?? email),
                new Claim(ClaimTypes.Email, result.Email ?? email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
