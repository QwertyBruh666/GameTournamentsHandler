using Application.BusinessLogic.Auth.Commands;
using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.BusinessLogic.Auth.EventHandlers;

public class LogoutCommandHandler(IAppDbContext appDbContext, IAuthTokensHandler tokensHandler) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (request.RefreshToken == null)
            throw new Exception("You are already logged out");
        await tokensHandler.ValidateTokenAsync(request.RefreshToken);
        var tokenFromDb = await appDbContext.AuthTokens.FirstOrDefaultAsync(t => t.Jti == tokensHandler.GetClaim("Authentication"), cancellationToken);
        
        if (tokenFromDb == null)
            throw new Exception("Session doesn't exist");
        
        appDbContext.AuthTokens.Remove(tokenFromDb);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}