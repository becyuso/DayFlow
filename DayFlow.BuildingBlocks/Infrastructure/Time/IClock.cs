namespace DayFlow.BuildingBlocks.Infrastructure.Time
{
    public interface IClock
    {
        DateTime UtcNow { get; }

        DateTime TaiwanNow { get; }
    }
}
