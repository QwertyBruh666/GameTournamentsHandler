using Application.BusinessLogic.Tournaments.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Tournaments.EventHandlers;

public class UpdateTournamentCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Tournament> auth) : IRequestHandler<UpdateTournamentCommand>
{
    public async Task Handle(UpdateTournamentCommand request, CancellationToken cancellationToken)
    {
        Tournament tournament = await auth.GetAuthorizedAsync(request.TournamentId, currUser.GetUserId(), cancellationToken);
        
    }
}