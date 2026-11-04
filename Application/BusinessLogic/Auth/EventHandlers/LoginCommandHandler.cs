using Application.BusinessLogic.Auth.Commands;
using Application.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Auth.EventHandlers;

public class LoginCommandHandler(IAppDbContext appDbContext, IAuthTokensHandler tokensHandler, IPasswordHasher passwordHasher) : IRequestHandler<LoginCommand, Dictionary<string, string>>
{
    public async Task<Dictionary<string, string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Credentials.Email && u.UserName == request.Credentials.UserName && u.PasswordHash == passwordHasher.GetHashed(request.Credentials.Password), cancellationToken);
        if (user == null)
            throw new Exception("Invalid username or password");
        
        Dictionary<string, string> tokens = tokensHandler.CreateTokens(user.Id, out string refreshJti);
        
        await appDbContext.AuthTokens.AddAsync(new AuthToken { Jti = refreshJti, UserId = user.Id }, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        
        return tokens;
    }
}