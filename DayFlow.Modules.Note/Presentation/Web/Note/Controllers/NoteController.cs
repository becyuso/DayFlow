using Microsoft.AspNetCore.Mvc;

namespace DayFlow.Modules.Notes.Presentation.Web.Note.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
