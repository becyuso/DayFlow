using Microsoft.AspNetCore.Mvc;

namespace DayFlow.Web.UI.Components.Tiptap;

public sealed class TiptapViewComponent
    : ViewComponent
{
    public IViewComponentResult Invoke(
        TiptapViewModel model)
    {
        return View("~/Components/Tiptap/Default.cshtml", model);
    }
}