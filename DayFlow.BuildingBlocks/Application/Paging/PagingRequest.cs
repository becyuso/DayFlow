namespace DayFlow.BuildingBlocks.Application.Paging;

public sealed record PagingRequest(
    int Page = 1,
    int PageSize = 20)
{
    public int Skip => (Page - 1) * PageSize;

    public int Take => PageSize;
}