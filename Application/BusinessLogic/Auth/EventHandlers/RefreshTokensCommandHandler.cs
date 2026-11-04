using Application.BusinessLogic.Auth.Commands;
using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.BusinessLogic.Auth.EventHandlers;

public class RefreshTokensCommandHandler(IAppDbContext appDbContext, IAuthTokensHandler tokensHandler) : IRequestHandler<RefreshTokensCommand, Dictionary<string, string>>
{
    public async Task<Dictionary<string, string>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        if(request.RefreshToken == null)
            throw new Exception("Refresh token doesn't exist");
        
        bool isValid = await tokensHandler.ValidateTokenAsync(request.RefreshToken);
        
        if (!isValid)
            throw new Exception("Refresh token doesn't valid");

        Console.WriteLine(tokensHandler.GetClaim("Authentication"));
        var tokenFromDb = await appDbContext.AuthTokens.FirstOrDefaultAsync(t => t.Jti == tokensHandler.GetClaim("Authentication"), cancellationToken);
        
        if (tokenFromDb == null)
            throw new Exception("Refresh token doesn't exist");
        
        string? claimVal = tokensHandler.GetClaim("Name");
        if (claimVal == null)
        {
            throw new Exception("Refresh token doesn't exist");
        }
        Guid userId  = Guid.Parse(claimVal);
        var tokens = tokensHandler.CreateTokens(userId, out string refreshJti);

        tokenFromDb.Jti = refreshJti;
        await appDbContext.SaveChangesAsync(cancellationToken);
        return tokens;
    }
}