using MediatR;

namespace DayFlow.Modules.Note.Application.Features.Notebook.Create
{
    public sealed record CreateCommand(Guid? UserId, string? Name, string? Color, int? SortOrder) : IRequest<CreateResult>;

    public sealed record CreateResult
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }
        public Guid? NotebookId { get; init; }

        public static CreateResult Fail(string message) => new() { Success = false, ErrorMessage = message };
        public static CreateResult Ok(Guid id) => new() { Success = true, NotebookId = id };
    }
}
