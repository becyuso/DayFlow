namespace DayFlow.Web.UI.Components.Pagination
{
    public sealed class PaginationViewModel
    {
        public string Action { get; init; } = "Index";

        public int Page { get; init; }

        public int PageSize { get; init; }

        public int TotalCount { get; init; }

        public Dictionary<string, string?> RouteValues { get; init; } = new();

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);

        public bool HasPrevious =>
            Page > 1;

        public bool HasNext =>
            Page < TotalPages;

        public Dictionary<string, string?> GetRouteValues(
            int page)
        {
            var values =
                new Dictionary<string, string?>(
                    RouteValues);


            values["page"] =
                page.ToString();


            return values;
        }
    }
}
