using Microsoft.AspNetCore.Mvc;

namespace DayFlow.Web.UI.Components.Toast;

public sealed class ToastViewComponent
    : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Components/Toast/Default.cshtml");
    }
}