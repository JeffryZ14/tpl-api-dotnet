using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Behaviors
{
    public interface ICacheableQuery<TResponse>
    {
        string CacheKey { get; }
        MemoryCacheEntryOptions? CacheOptions { get; }
    }

    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableQuery<TResponse>
    {
        private readonly IMemoryCache _cache;

        public CachingBehavior(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(request.CacheKey, out TResponse cachedResponse))
            {
                return cachedResponse;
            }

            var response = await next();

            var options = request.CacheOptions ?? new MemoryCacheEntryOptions();
            _cache.Set(request.CacheKey, response, options);

            return response;
        }
    }
}
