namespace DayFlow.BuildingBlocks.Application.Paging
{
    public interface IPagedResult
    {
        int Page { get; }

        int PageSize { get; }

        int TotalCount { get; }
    }
}
