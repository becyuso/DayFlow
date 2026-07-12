using MediatR;

namespace DayFlow.Modules.Note.Application.Features.Noteboke.CreateNotebook
{
    public static class CreateNotebook
    {
        public sealed record Command(System.Guid? UserId, string? Name, string? Color, int? SortOrder) : IRequest<Result>;

        public sealed record Result
        {
            public bool Success { get; init; }
            public string? ErrorMessage { get; init; }
            public Guid? NotebookId { get; init; }

            public static Result Fail(string message) => new() { Success = false, ErrorMessage = message };
            public static Result Ok(Guid id) => new() { Success = true, NotebookId = id };
        }
    }
}
