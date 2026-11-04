using Application.BusinessLogic.TournamentsMembers.Commands;
using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.BusinessLogic.TournamentsMembers.EventHandlers;

public class UpdateTournamentMemberStatusCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<TournamentMember> auth) : IRequestHandler<UpdateTournamentMemberStatusCommand, Guid>
{
    public async Task<Guid> Handle(UpdateTournamentMemberStatusCommand request, CancellationToken cancellationToken)
    {
        TournamentMember member = await auth.GetAuthorizedAsync(request.MemberId, currUser.GetUserId(), cancellationToken);
        member.Status = request.NewMemberStatus;
        await appDbContext.SaveChangesAsync(cancellationToken);
        return member.Id;
    }
}