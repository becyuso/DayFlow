namespace DayFlow.BuildingBlocks.Application.Results
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string? Code { get; }
        string? Message { get; }
    }
}
