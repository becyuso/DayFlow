namespace DayFlow.BuildingBlocks.Application.Messages
{
    public interface IMessageRegistry
    {
        void Add(string code, string message);
    }
}
