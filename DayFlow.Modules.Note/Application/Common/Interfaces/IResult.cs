namespace DayFlow.Modules.Notes.Application.Common.Interfaces
{
    public interface IResult
    {
        bool Success { get; }
        string? Message { get; }
    }
}
