using Application.BusinessLogic.Games.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Games.EventHandlers;

public class CreateGameCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<CreateGameCommand, Guid>
{
    public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        bool isEnoughRights =
            await appDbContext.Users.AnyAsync(u => u.Id == currUser.GetUserId() && u.UserRole == UserRole.Admin,
                cancellationToken);

        if (!isEnoughRights)
            throw new Exception("You dont have enough rights");

        Game newGame = new Game
        {
            Name = request.Game.Name,
            Description = request.Game.Description,
            IsActive = request.Game.IsActive
        };
        
        await appDbContext.Games.AddAsync(newGame, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return newGame.Id;
    }
}