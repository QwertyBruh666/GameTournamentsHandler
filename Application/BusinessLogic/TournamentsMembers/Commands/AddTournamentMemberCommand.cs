using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.TournamentsMembers.Commands;

public record AddTournamentMemberCommand(Guid TournamentId, Guid NewMemberTeamId) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => "tournament-members";
}