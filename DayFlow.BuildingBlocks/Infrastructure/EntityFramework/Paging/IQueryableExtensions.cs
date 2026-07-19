using DayFlow.BuildingBlocks.Application.Paging;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.BuildingBlocks.Infrastructure.EntityFramework.Paging;

public static class IQueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        PagingRequest paging,
        CancellationToken cancellationToken = default)
    {
        var totalCount =
            await query.CountAsync(cancellationToken);


        var items =
            await query
                .Skip(paging.Skip)
                .Take(paging.Take)
                .ToListAsync(cancellationToken);


        return new PagedResult<T>
        {
            Items = items,
            Page = paging.Page,
            PageSize = paging.PageSize,
            TotalCount = totalCount
        };
    }
}