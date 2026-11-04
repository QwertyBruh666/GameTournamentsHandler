namespace Application.Contracts;

public interface IPasswordHasher
{
    string GetHashed(string value);
}