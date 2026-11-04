using Application.BusinessLogic.Games.Commands;
using Application.Contracts;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Games.EventHandlers;

public class UpdateActiveStatusCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<UpdateActiveStatusCommand>
{
    public async Task Handle(UpdateActiveStatusCommand request, CancellationToken cancellationToken)
    {
        var gameInfo = await appDbContext.Games
            .Where(g => 
                g.Id == request.GameId)
            .Select(g => new { Game = g, IsEnoughRights = appDbContext.Users.Any(u => u.Id == currUser.GetUserId() && u.UserRole == UserRole.Admin) })
            .FirstOrDefaultAsync(cancellationToken);
        
        if (gameInfo == null)
            throw new Exception("Game not found");
        
        if (!gameInfo.IsEnoughRights)
            throw new Exception("You don't have enough rights");
        
        gameInfo.Game.IsActive = request.IsActive;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}