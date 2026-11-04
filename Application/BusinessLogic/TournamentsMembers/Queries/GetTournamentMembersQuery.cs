using Application.BusinessLogic.TournamentsMembers.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.TournamentsMembers.Queries;

public record GetTournamentMembersQuery(Guid TournamentId) : IRequest<List<TournamentMemberDto>>, ICacheable
{
    public string GetCacheKey() => "tournament-members";

    public TimeSpan CacheDuration { get; } = TimeSpan.FromMinutes(5);
}