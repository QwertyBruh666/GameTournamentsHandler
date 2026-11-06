using Application.BusinessLogic.Matches.Commands;
using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class FinishMatchCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Match> auth) : IRequestHandler<FinishMatchCommand>
{
    public async Task Handle(FinishMatchCommand request, CancellationToken cancellationToken)
    {
        MatchResultRequestDto results = request.MatchResult;
        
        Match match = await auth.GetAuthorizedAsync(request.MatchId, currUser.GetUserId(), cancellationToken);
        
        if (match.Status != MatchStatus.InProgress)
            throw new Exception("Match is not in progress");
        
        TournamentMember winner;

        if (match.Participant1Id != null && match.Participant2Id != null 
                                         && match.Participant1?.Status != TournamentMemberStatus.Disqualified && match.Participant2?.Status != TournamentMemberStatus.Disqualified)
        {
            winner = results.Team1Score > results.Team2Score ? match.Participant1! : match.Participant2!;
            match.Participant1Score = results.Team1Score;
            match.Participant2Score = results.Team2Score;
        }
        else if (match.Participant1?.Status == TournamentMemberStatus.Disqualified || match.Participant2?.Status == TournamentMemberStatus.Disqualified)
            winner = match.Participant1!.Status == TournamentMemberStatus.Disqualified ? match.Participant2! : match.Participant1!;
        else if (match.Participant1Id == null || match.Participant2Id == null)
        {
            winner = match.Participant1 ?? match.Participant2!;
            if (winner.Id == match.Participant1Id)
                match.Participant1Score = 1;
            else
                match.Participant2Score = 0;
        }
        else
            throw new Exception("You need at least one participant to finish a match");

        if (match.NextMatchId == null)
        {
            await appDbContext.Matches.Entry(match).Reference(m => m.Tournament).LoadAsync(cancellationToken);
            match.Tournament!.Status = TournamentStatus.Finished;
            match.Tournament.WinnerId = winner.Id;
        }
        else
        {
            await appDbContext.Matches.Entry(match).Reference(m => m.NextMatch).LoadAsync(cancellationToken);
            if(match.Position % 2 == 0)
                match.NextMatch!.Participant2Id = winner.Id;
            else
                match.NextMatch!.Participant1Id = winner.Id;
        }
        
        match.Status = MatchStatus.Finished;

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}