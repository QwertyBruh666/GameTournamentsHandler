using Application.Contracts;
using MediatR;

namespace Application.Pipelines;

public class CacheBehavior<TRequest, TResponse>(ICacheProvider provider) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheable
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? cached = await provider.GetAsync<TResponse>(request.GetCacheKey());
        if(cached != null)
            return cached;
        
        TResponse response = await next(cancellationToken);
        
        await provider.SetAsync(response, request.GetCacheKey());
        return response;
    }
}