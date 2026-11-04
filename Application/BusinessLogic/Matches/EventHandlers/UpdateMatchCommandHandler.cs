using Application.BusinessLogic.Matches.Commands;
using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class UpdateMatchCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Match> auth) : IRequestHandler<UpdateMatchCommand>
{
    public async Task Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
    {
        UpdateMatchDto matchDto = request.Match;
        
        Match match = await auth.GetAuthorizedAsync(request.MatchId, currUser.GetUserId(), cancellationToken);
        
        match.Participant1Id = matchDto.Team1Id ?? match.Participant1Id;
        match.Participant2Id = matchDto.Team2Id ?? match.Participant2Id;
        match.ScheduleStartsAt = matchDto.StartsAt ?? match.ScheduleStartsAt;
        match.ScheduleEndsAt = matchDto.EndsAt ?? match.ScheduleEndsAt;
        match.Status = matchDto.Status ?? match.Status;
        match.TournamentId = matchDto.TournamentId ?? match.TournamentId;
        match.Round = matchDto.Round ?? match.Round;
        match.Position = matchDto.Position ?? match.Position;
        match.Participant1Score = matchDto.Team1Score ?? match.Participant1Score;
        match.Participant2Score = matchDto.Team2Score ?? match.Participant2Score;
        
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}