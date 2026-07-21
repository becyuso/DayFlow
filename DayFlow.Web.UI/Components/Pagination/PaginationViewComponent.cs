using Microsoft.AspNetCore.Mvc;

namespace DayFlow.Web.UI.Components.Pagination;

public sealed class PaginationViewComponent
    : ViewComponent
{
    /// <summary>
    /// add <AddRazorSupportForMvc>true</AddRazorSupportForMvc>
    /// add <EnableDefaultRazorGenerateItems>true</EnableDefaultRazorGenerateItems>
    /// 
    /// 原目標位置
    /// Views/
    ///    Shared/
    ///       Components/
    ///          Pagination/
    ///             Default.cshtml
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(
        PaginationViewModel model)
    {
        return View("~/Components/Pagination/Default.cshtml", model);
    }
}