using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.TournamentsMembers.Commands;

public record UpdateTournamentMemberStatusCommand(Guid MemberId, TournamentMemberStatus NewMemberStatus) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => "tournament-members";
}