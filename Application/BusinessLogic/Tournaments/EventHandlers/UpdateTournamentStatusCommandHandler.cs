using Application.BusinessLogic.Tournaments.Commands;
using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.BusinessLogic.Tournaments.EventHandlers;

public class UpdateTournamentStatusCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Tournament> auth) : IRequestHandler<UpdateTournamentStatusCommand, Guid>
{
    public async Task<Guid> Handle(UpdateTournamentStatusCommand request, CancellationToken cancellationToken)
    {
        Tournament tournament = await auth.GetAuthorizedAsync(request.TournamentId, currUser.GetUserId(), cancellationToken);
        if (request.NewStatus - tournament.Status != 1)
            throw new Exception("Wrong status transition");
        tournament.Status = request.NewStatus;
        await appDbContext.SaveChangesAsync(cancellationToken);
        return tournament.Id;
    }
}