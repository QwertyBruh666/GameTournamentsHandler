using Domain.Entities;

namespace Application.Contracts;

public interface IAccessService<T> where T : BaseEntity
{ 
    Task<T> GetAuthorizedAsync(Guid id, Guid userId, CancellationToken cancellationToken);
}