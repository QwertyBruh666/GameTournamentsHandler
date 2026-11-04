using Application.BusinessLogic.Tournaments.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Tournaments.EventHandlers;

public class RemoveTournamentCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Tournament> auth) : IRequestHandler<RemoveTournamentCommand>
{
    public async Task Handle(RemoveTournamentCommand request, CancellationToken cancellationToken)
    {
        Tournament tournament = await auth.GetAuthorizedAsync(request.TournamentId, currUser.GetUserId(), cancellationToken);
        appDbContext.Tournaments.Remove(tournament);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}