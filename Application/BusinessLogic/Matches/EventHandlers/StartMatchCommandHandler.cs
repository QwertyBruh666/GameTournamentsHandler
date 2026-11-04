using Application.BusinessLogic.Matches.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class StartMatchCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Match> auth) : IRequestHandler<StartMatchCommand, Guid>
{
    public async Task<Guid> Handle(StartMatchCommand request, CancellationToken cancellationToken)
    {
        Match match = await auth.GetAuthorizedAsync(request.MatchId, currUser.GetUserId(), cancellationToken);

        if (match.Tournament!.Status != TournamentStatus.InProgress)
            throw new Exception("Cannot start match because tournament is not in progress");
        
        match.Status = MatchStatus.InProgress;
        match.ActualStartsAt = DateTime.UtcNow;
        await appDbContext.SaveChangesAsync(cancellationToken);
        return match.Id;
    }
}