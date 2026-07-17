//HybridCache(.NET 9)

//using MediatR;
//using Microsoft.Extensions.Caching.Distributed;
//using Newtonsoft.Json;

//namespace DayFlow.BuildingBlocks.Application.Behaviors
//{
//    public class CachingBehavior<TRequest, TResponse>
//        : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//    {
//        private readonly IDistributedCache _cache;

//        public CachingBehavior(
//            IDistributedCache cache)
//        {
//            _cache = cache;
//        }

//        public async Task<TResponse> Handle(
//            TRequest request,
//            RequestHandlerDelegate<TResponse> next,
//            CancellationToken cancellationToken)
//        {
//            if (request is not ICacheable cacheable)
//                return await next();

//            var json =
//                await _cache.GetStringAsync(
//                    cacheable.CacheKey);

//            if (json != null)
//            {
//                return JsonSerializer
//                    .Deserialize<TResponse>(json)!;
//            }

//            var response =
//                await next();

//            await _cache.SetStringAsync(
//                cacheable.CacheKey,
//                JsonSerializer.Serialize(response));

//            return response;

//        }
//    }
//}