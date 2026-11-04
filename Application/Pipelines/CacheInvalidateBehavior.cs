using Application.Contracts;
using MediatR;

namespace Application.Pipelines;

public class CacheInvalidateBehavior<TRequest, TResponse>(ICacheProvider provider) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheInvalidator
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next(cancellationToken);
        await provider.RemoveAsync(request.GetCacheKey());
        return response;
    }
}