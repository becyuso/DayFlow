namespace DayFlow.BuildingBlocks.Time
{
    public interface IClock
    {
        DateTime UtcNow { get; }

        DateTime TaiwanNow { get; }
    }
}
