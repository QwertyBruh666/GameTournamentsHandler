using Application.BusinessLogic.Matches.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class StartMatchCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Match> auth) : IRequestHandler<StartMatchCommand>
{
    public async Task Handle(StartMatchCommand request, CancellationToken cancellationToken)
    {
        Match match = await auth.GetAuthorizedAsync(request.MatchId, currUser.GetUserId(), cancellationToken);
        bool isPrevFinished = await appDbContext.Matches.Where(m => m.NextMatchId == match.Id)
            .AllAsync(m => m.Status == MatchStatus.Finished);
        if(match.Round != 1 && !isPrevFinished)
            throw new Exception("Previous matches arent finished");
        
        match.Status = MatchStatus.InProgress;
        match.ActualStartsAt = DateTime.UtcNow;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}