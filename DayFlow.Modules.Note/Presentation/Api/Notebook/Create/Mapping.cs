using DayFlow.Modules.Notes.Application.Features.Notebook.Create;
using DayFlow.Modules.Notes.Presentation.Api.Notebook.Create;

namespace DayFlow.Modules.Identity.Presentation.Api.Notebook.Create
{
    public static class Mapping
    {
        public static CreateCommand ToCommand(
            this CreateRequest request)
        {
            return new CreateCommand
            (
                request.UserId,
                request.Name,
                request.Color,
                request.SortOrder
            );
        }

        public static CreateResponse ToResponse(
            this CreateResult result)
        {
            return new CreateResponse
            (
            );
        }
    }
}