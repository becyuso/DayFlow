namespace DayFlow.BuildingBlocks.Application.Messages
{
    public interface IMessageRegistrar
    {
        void Register(IMessageRegistry registry);
    }
}
