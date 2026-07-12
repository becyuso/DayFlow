using MediatR;

namespace DayFlow.Modules.Note.Application.Features.Noteboke.DeleteNotebook
{
    public static class DeleteNotebook
    {
        public sealed record Command(System.Guid NotebookId, System.Guid UserId) : IRequest<Result>;

        public sealed record Result
        {
            public bool Success { get; init; }
            public string? ErrorMessage { get; init; }

            public static Result Fail(string message) => new() { Success = false, ErrorMessage = message };
            public static Result Ok() => new() { Success = true };
        }
    }
}
