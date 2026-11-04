using Application.BusinessLogic.Auth.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.BusinessLogic.Auth.EventHandlers;

public class RegisterCommandHandler(IAppDbContext appDbContext, IAuthTokensHandler tokensHandler, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, Dictionary<string, string>>
{
    public async Task<Dictionary<string, string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if ((request.RegisterInfo.Role == UserRole.Admin || request.RegisterInfo.Role == UserRole.Organizer) && request.RegisterInfo.AdminKey != "HjK-006-DfG777")
            throw new Exception("You cannot register as an admin user");
        
        User? newUser = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.RegisterInfo.Credentials.Email || u.UserName == request.RegisterInfo.Credentials.UserName, cancellationToken);
        
        if (newUser != null)
            throw new Exception("User already exists");
        
        newUser = new User { Email = request.RegisterInfo.Credentials.Email, UserName = request.RegisterInfo.Credentials.UserName, UserRole = request.RegisterInfo.Role, PasswordHash = passwordHasher.GetHashed(request.RegisterInfo.Credentials.Password)};
        Dictionary<string, string> tokens = tokensHandler.CreateTokens(newUser.Id, out string refreshJti);
        
        await appDbContext.Users.AddAsync(newUser, cancellationToken);
        await appDbContext.AuthTokens.AddAsync(new AuthToken { Jti = refreshJti, UserId = newUser.Id }, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        
        return tokens;
    }
}