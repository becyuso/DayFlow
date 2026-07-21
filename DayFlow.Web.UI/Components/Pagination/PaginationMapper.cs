using DayFlow.BuildingBlocks.Application.Paging;
using Microsoft.AspNetCore.Routing;

namespace DayFlow.Web.UI.Components.Pagination
{
    public static class PaginationMapper
    {
        public static PaginationViewModel ToPagination<T>(
            this PagedResult<T> result,
            string action,
            object? routeValues = null)
        {
            return new()
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                Action = action,
                RouteValues =
                routeValues == null
                    ? new()
                    : new Dictionary<string, string?>(
                        new RouteValueDictionary(routeValues)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Value?.ToString()))
            };
        }
    }
}
