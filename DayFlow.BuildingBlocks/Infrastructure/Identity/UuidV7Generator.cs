namespace DayFlow.BuildingBlocks.Infrastructure.Identity;

public sealed class UuidV7Generator : IIdGenerator
{

    public Guid NewId()
    {
        return Guid.CreateVersion7();
    }

}