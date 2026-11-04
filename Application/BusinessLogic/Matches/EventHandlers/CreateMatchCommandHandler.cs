using Application.BusinessLogic.Matches.Commands;
using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class CreateMatchCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<CreateMatchCommand, Guid>
{
    public async Task<Guid> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        CreateMatchDto matchDto = request.Match;
        if (!await appDbContext.Tournaments.AnyAsync(t => t.Id == request.TournamentId && t.OrganizerId == currUser.GetUserId(), cancellationToken))
            throw new Exception("You dont have enough rights");

        var match = await appDbContext.Matches
            .FirstOrDefaultAsync(
                m => 
                    m.TournamentId == request.TournamentId && 
                    m.Round == 1 &&
                    m.ScheduleStartsAt == null &&
                    m.ScheduleEndsAt == null &&
                    m.Participant1Id == null &&
                    m.Participant2Id == null, cancellationToken) ?? throw new Exception("There is no empty match");

        match.ScheduleStartsAt = matchDto.StartsAt;
        match.ScheduleEndsAt = matchDto.EndsAt;
        match.Participant1Id = matchDto.Team1Id;
        match.Participant2Id = matchDto.Team2Id;
        
        await appDbContext.SaveChangesAsync(cancellationToken);
        
        return match.Id;
    }
}