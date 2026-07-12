using MediatR;

namespace DayFlow.Modules.Note.Application.Features.Notebook.Update
{
    public sealed record UpdateCommand(System.Guid NotebookId, System.Guid UserId, string Name, string? Color, int SortOrder) : IRequest<UpdateResult>;

    public sealed record UpdateResult
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }

        public static UpdateResult Fail(string message) => new() { Success = false, ErrorMessage = message };
        public static UpdateResult Ok() => new() { Success = true };
    }
}
