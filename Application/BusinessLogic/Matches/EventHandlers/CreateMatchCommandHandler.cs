using Application.BusinessLogic.Matches.Commands;
using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class CreateMatchCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<CreateMatchCommand, Guid>
{
    public async Task<Guid> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        CreateMatchDto matchDto = request.Match;
        Tournament? tour = await  appDbContext.Tournaments.FindAsync(request.TournamentId, cancellationToken) ?? throw new Exception("Tournament not found");
        
        if (tour.OrganizerId != currUser.GetUserId())
            throw new Exception("You dont have enough rights");
        
        if (request.Match.StartsAt < tour.StartsAt || request.Match.EndsAt > tour.EndsAt)
            throw new Exception("You cant start match before tournament");

        var match = await appDbContext.Matches
            .FirstOrDefaultAsync(
                m => 
                    m.TournamentId == request.TournamentId && 
                    m.Round == request.Match.Round &&
                    m.Position == request.Match.Position, cancellationToken) ?? throw new Exception("Match not found");
        
        match.ScheduleStartsAt = matchDto.StartsAt;
        match.ScheduleEndsAt = matchDto.EndsAt;
        match.Participant1Id = matchDto.Team1Id;
        match.Participant2Id = matchDto.Team2Id;
        
        await appDbContext.SaveChangesAsync(cancellationToken);
        
        return match.Id;
    }
}