using Application.BusinessLogic.Matches.Commands;
using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class RemoveMatchCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Match> auth) : IRequestHandler<RemoveMatchCommand>
{
    public async Task Handle(RemoveMatchCommand request, CancellationToken cancellationToken)
    {
        Match match = await auth.GetAuthorizedAsync(request.MatchId, currUser.GetUserId(), cancellationToken);
        
        appDbContext.Matches.Remove(match);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}