using Application.Contracts;
using Domain.Entities;

namespace Application.BusinessLogic.Access.Services;

public abstract class BaseAccessService<T> : IAccessService<T> where T : BaseEntity
{
    public abstract Task<T> GetAuthorizedAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    protected T CheckAnswer(AccessEntityDto<T>? entity)
    {
        if (entity == null)
            throw new Exception("Not found");

        if (!entity.IsEnoughRights)
            throw new Exception("You dont have enough rights");

        return entity.Entity!;
    }
}