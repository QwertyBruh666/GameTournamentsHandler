namespace Application.Contracts;

public interface IAuthTokensHandler
{
    Dictionary<string, string> CreateTokens(Guid userId, out string jti);
    Task<bool> ValidateTokenAsync(string token);
    string? GetClaim(string claimType);
}