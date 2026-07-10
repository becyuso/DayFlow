namespace DayFlow.Api.Diagnostics
{
    public static class EndpointDiagnosticsExtensions
    {
        public static WebApplication LogEndpoints(this WebApplication app)
        {
            var dataSources = ((IEndpointRouteBuilder)app).DataSources;
            foreach (var ds in dataSources)
            {
                foreach (var endpoint in ds.Endpoints)
                {
                    Console.WriteLine("api endpoints list:");
                    Console.WriteLine(endpoint.DisplayName);
                    Console.WriteLine();
                }
            }

            return app;
        }
    }
}
