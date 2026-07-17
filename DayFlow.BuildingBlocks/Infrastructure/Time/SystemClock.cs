namespace DayFlow.BuildingBlocks.Infrastructure.Time
{
    public class SystemClock : IClock
    {
        public DateTime UtcNow =>
            DateTime.UtcNow;

        public DateTime TaiwanNow =>
            DateTime.UtcNow.AddHours(8);
    }
}
