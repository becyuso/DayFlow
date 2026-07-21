using DayFlow.BuildingBlocks.Application.Paging;
using DayFlow.Modules.Notes.Application.Features.Notebook.Create;
using DayFlow.Modules.Notes.Application.Features.Notebook.Delete;
using DayFlow.Modules.Notes.Application.Features.Notebook.List;
using DayFlow.Modules.Notes.Application.Features.Notebook.Update;
using DayFlow.Modules.Notes.Presentation.Web.Notebook.Mapping;
using DayFlow.Modules.Notes.Presentation.Web.Notebook.ViewModels;
using DayFlow.Web.UI.Components.Toast;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DayFlow.Modules.Notes.Presentation.Web.Notebook.Controllers
{
    [Authorize]
    public class NotebookController : Controller
    {
        private readonly IMediator _mediator;
        public NotebookController(IMediator mediator) => _mediator = mediator;

        public async Task<IActionResult> Index(
            string? keyword = null,
            int page = 1)
        {
            var userId = GetCurrentUserId();

            var result = await _mediator.Send(
                new ListQuery(
                    userId,
                    keyword,
                    new PagingRequest(page, 20)));

            if (!result.Success || result.Data is null)
            {
                return View(new NotebookListViewModel());
            }

            return View(result.Data.ToViewModel(keyword));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new NotebookEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NotebookEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = GetCurrentUserId();

            var cmd = new CreateCommand(userId == Guid.Empty ? null : userId, model.Name, model.Color, model.SortOrder);
            var result = await _mediator.Send(cmd);

            if (result == null || !result.Success)
            {
                TempData.ToastError("筆記建立失敗");
                //ModelState.AddModelError(string.Empty, result?.Message ?? "Failed to create notebook");
                return View(model);
            }

            TempData.ToastSuccess("筆記建立成功");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Incomplete: when GetNotebook.Query exists, use it to fill the view model
            return View(new NotebookEditViewModel { NotebookId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NotebookEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.NotebookId == null || model.NotebookId == Guid.Empty)
                return BadRequest();

            var userId = GetCurrentUserId();

            var cmd = new UpdateCommand(model.NotebookId.Value, userId, model.Name, model.Color, model.SortOrder);
            var result = await _mediator.Send(cmd);

            if (!result.Success)
            {
                TempData.ToastError("筆記更新失敗");
                //ModelState.AddModelError(string.Empty, result.Message ?? "Failed to update notebook");
                return View(model);
            }

            TempData.ToastSuccess("筆記更新成功");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new NotebookEditViewModel { NotebookId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid notebookId)
        {
            var userId = GetCurrentUserId();
            var cmd = new DeleteCommand(notebookId, userId);
            var result = await _mediator.Send(cmd);

            if (!result.Success)
            {
                TempData.ToastError("筆記刪除失敗");
                return RedirectToAction(nameof(Delete), new { id = notebookId });
            }

            TempData.ToastSuccess("筆記刪除成功");
            return RedirectToAction(nameof(Index));
        }

        private Guid GetCurrentUserId()
        {
            // Try common claim types; return Guid.Empty when not found
            var idClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User?.FindFirst("sub")?.Value;

            if (Guid.TryParse(idClaim, out var userId))
                return userId;

            return Guid.Empty;
        }
    }
}
