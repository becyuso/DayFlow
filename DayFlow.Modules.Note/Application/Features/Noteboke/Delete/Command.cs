using MediatR;

namespace DayFlow.Modules.Note.Application.Features.Notebook.Delete
{
    public sealed record DeleteCommand(System.Guid NotebookId, System.Guid UserId) : IRequest<DeleteResult>;

    public sealed record DeleteResult
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }

        public static DeleteResult Fail(string message) => new() { Success = false, ErrorMessage = message };
        public static DeleteResult Ok() => new() { Success = true };
    }
}
