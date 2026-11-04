namespace Application.Contracts;

public interface ICurrentUser
{
    Guid GetUserId();
}