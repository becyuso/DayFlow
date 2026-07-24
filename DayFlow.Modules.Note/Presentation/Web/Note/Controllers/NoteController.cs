using DayFlow.BuildingBlocks.Application.Paging;
using DayFlow.Modules.Notes.Application.Features.Note.Create;
using DayFlow.Modules.Notes.Application.Features.Note.Delete;
using DayFlow.Modules.Notes.Application.Features.Note.List;
using DayFlow.Modules.Notes.Application.Features.Note.Update;
using DayFlow.Modules.Notes.Presentation.Web.Note.Mapping;
using DayFlow.Modules.Notes.Presentation.Web.Note.ViewModels;
using DayFlow.Modules.Notes.Presentation.Web.Notebook.Controllers;
using DayFlow.Web.UI.Components.Toast;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

#region using alias
using NoteGet =
    DayFlow.Modules.Notes.Application.Features.Note.Get;
using NotebookGet =
    DayFlow.Modules.Notes.Application.Features.Notebook.Get;
#endregion

namespace DayFlow.Modules.Notes.Presentation.Web.Note.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator) => _mediator = mediator;

        public async Task<IActionResult> Index(
            Guid notebookId,
            int page = 1)
        {
            if (notebookId == Guid.Empty)
                return RedirectToAction(
                    nameof(NotebookController.Index),
                    nameof(NotebookController).Replace(nameof(Controller), ""));

            var userId = GetCurrentUserId();

            var notebookResult = await _mediator.Send(
                new NotebookGet.GetQuery(userId, notebookId));

            if (!notebookResult.Success || notebookResult.Data is null)
                return RedirectToAction(
                        nameof(NotebookController.Index),
                        nameof(NotebookController).Replace(nameof(Controller), ""));

            var result = await _mediator.Send(
                new ListQuery(
                    userId,
                    notebookId,
                    null,
                    new PagingRequest(page, 20)));

            if (!result.Success || result.Data is null)
                return View(new NoteListViewModel());

            return View(result.Data.ToViewModel(notebookId, notebookResult.Data.Name));
        }

        [HttpGet]
        public async Task<IActionResult> Create(
            Guid notebookId)
        {
            var userId = GetCurrentUserId();

            // notebook
            var notebookResult = await _mediator.Send(
                new NotebookGet.GetQuery(userId, notebookId));

            if (!notebookResult.Success || notebookResult.Data is null)
                return RedirectToAction(
                        nameof(NotebookController.Index),
                        nameof(NotebookController).Replace(nameof(Controller), ""));

            return View(new NoteEditViewModel
            {
                NotebookName = notebookResult.Data.Name,
                NotebookOptions = await GetNotebookOptions(userId)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteEditViewModel model)
        {
            var userId = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                model.NotebookOptions = await GetNotebookOptions(userId);
                return View(model);
            }

            var command = new CreateCommand(
                userId,
                model.NotebookId,
                model.Title,
                model.Content,
                model.Summary);

            var result = await _mediator.Send(command);

            if (result == null || !result.Success)
            {
                TempData.ToastError("筆記建立失敗");

                model.NotebookOptions = await GetNotebookOptions(userId);

                return View(model);
            }

            TempData.ToastSuccess("筆記建立成功");

            return RedirectToAction(nameof(Index), new
            {
                notebookId = model.NotebookId
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(
            Guid notebookId,
            Guid id)
        {
            var userId = GetCurrentUserId();

            // notebook
            var notebookResult = await _mediator.Send(
                new NotebookGet.GetQuery(userId, notebookId));

            if (!notebookResult.Success || notebookResult.Data is null)
                return RedirectToAction(
                        nameof(NotebookController.Index),
                        nameof(NotebookController).Replace(nameof(Controller), ""));

            // note
            var result = await _mediator.Send(
                new NoteGet.GetQuery(userId, id));

            if (result == null || !result.Success || result.Data == null)
            {
                TempData.ToastInfo("操作逾時，請重新操作");
                return RedirectToAction(nameof(Index), new
                {
                    notebookId = notebookId
                });
            }

            return View(new NoteEditViewModel
            {
                NoteId = result.Data.NoteId,
                NotebookId = result.Data.NotebookId,
                NotebookName = notebookResult.Data.Name,
                Title = result.Data.Title,
                Content = result.Data.Content,
                Summary = result.Data.Summary,
                NotebookOptions = await GetNotebookOptions(userId)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NoteEditViewModel model)
        {
            var userId = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                model.NotebookOptions = await GetNotebookOptions(userId);
                return View(model);
            }

            if (model.NoteId == null || model.NoteId == Guid.Empty)
            {
                return BadRequest();
            }

            var command = new UpdateCommand(
                userId,
                model.NoteId.Value,
                model.Title,
                model.Content,
                model.Summary);

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                TempData.ToastError("筆記更新失敗");

                model.NotebookOptions = await GetNotebookOptions(userId);

                return View(model);
            }

            TempData.ToastSuccess("筆記更新成功");

            return RedirectToAction(nameof(Index), new
            {
                notebookId = model.NotebookId
            });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(
            Guid notebookId,
            Guid id)
        {
            var userId = GetCurrentUserId();

            // notebook
            var notebookResult = await _mediator.Send(
                new NotebookGet.GetQuery(userId, notebookId));

            if (!notebookResult.Success || notebookResult.Data is null)
                return RedirectToAction(
                        nameof(NotebookController.Index),
                        nameof(NotebookController).Replace(nameof(Controller), ""));

            // note
            var result = await _mediator.Send(
                new NoteGet.GetQuery(userId, id));

            if (result == null || !result.Success || result.Data == null)
            {
                TempData.ToastInfo("操作逾時，請重新操作");
                return RedirectToAction(nameof(Index), new
                {
                    notebookId = notebookId
                });
            }

            return View(new NoteEditViewModel
            {
                NoteId = result.Data.NoteId,
                NotebookId = notebookResult.Data.NotebookId,
                NotebookName = notebookResult.Data.Name,
                Title = result.Data.Title
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            Guid notebookId,
            Guid noteId)
        {
            var userId = GetCurrentUserId();

            var result = await _mediator.Send(
                new DeleteCommand(noteId, userId));

            if (!result.Success)
            {
                TempData.ToastError("筆記刪除失敗");
                return RedirectToAction(nameof(Delete), new { notebookId = notebookId });
            }

            TempData.ToastSuccess("筆記刪除成功");

            return RedirectToAction(nameof(Index), new
            {
                notebookId = notebookId
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetNotebookOptions(Guid userId)
        {
            var result = await _mediator.Send(
                new DayFlow.Modules.Notes.Application.Features.Notebook.List.ListQuery(
                    userId,
                    null,
                    new PagingRequest(1, 1000)));

            if (!result.Success || result.Data == null)
            {
                return Enumerable.Empty<SelectListItem>();
            }

            return result.Data.Items
                .OrderBy(x => x.SortOrder)
                .Select(x => new SelectListItem
                {
                    Value = x.NotebookId.ToString(),
                    Text = x.Name
                });
        }

        private Guid GetCurrentUserId()
        {
            var idClaim =
                User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User?.FindFirst("sub")?.Value;

            if (Guid.TryParse(idClaim, out var userId))
            {
                return userId;
            }

            return Guid.Empty;
        }
    }
}