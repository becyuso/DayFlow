namespace DayFlow.Modules.Notes.Presentation.Api.Notebook.Create
{
    public sealed record CreateRequest(Guid? UserId, string? Name, string? Color, int? SortOrder);

}
