namespace DayFlow.Modules.Notes.Domain.Tags
{
    public interface ITagRepository
    {
        Task<Tag?> GetByTagIdAsync(Guid? tagId);
    }
}
