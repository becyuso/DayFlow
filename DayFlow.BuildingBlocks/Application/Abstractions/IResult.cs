namespace DayFlow.BuildingBlocks.Application.Results
{
    public interface IResult
    {
        bool Success { get; }
        string? Message { get; }
    }
}
