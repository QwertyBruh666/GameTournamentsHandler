using Application.BusinessLogic.Tournaments.Commands;
using Application.BusinessLogic.Tournaments.Dtos;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Tournaments.EventHandlers;

public class CreateTournamentCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<CreateTournamentCommand, Guid>
{
    private void CreateMatchGrid(Guid tournamentId, Match match, int currRound, List<Match> matches)
    {
        if(currRound <= 0)
            return;
        
        match.TournamentId = tournamentId;
        match.Round = currRound;
        
        matches.Add(match);
        
        Match daughterMatch1 = new Match
        {
            TournamentId = tournamentId,
            NextMatchId =  match.Id,
        };
        Match daughterMatch2 = new Match
        {
            TournamentId = tournamentId,
            NextMatchId =  match.Id,
        };
        
        CreateMatchGrid(tournamentId, daughterMatch1, currRound - 1, matches);
        CreateMatchGrid(tournamentId, daughterMatch2, currRound - 1, matches);
    }

    private void SetPositions(List<Match> matches, int roundCount)
    {
        List<int> currPossesInRound = Enumerable.Repeat(1, roundCount).ToList();
        foreach (var match in matches)
        {
            match.Position = currPossesInRound[match.Round!.Value - 1];
            currPossesInRound[match.Round!.Value - 1] += 1;
        }
    }

    public async Task<Guid> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
    {
        CreateTournamentDto tournamentDto = request.Tournament;
        bool isEnoughRights = await appDbContext.Users.AnyAsync(u => u.Id == currUser.GetUserId() && u.UserRole >= UserRole.Organizer, cancellationToken);
        
        if (!isEnoughRights)
            throw new Exception("You don't have enough rights");
        
        var tournament = new Tournament
        {
            OrganizerId = currUser.GetUserId(), 
            Name = tournamentDto.Name, 
            Description = tournamentDto.Description, 
            GameId = tournamentDto.GameId, 
            RegistrationOpensAt = tournamentDto.RegistrationOpensAt, 
            RegistrationClosesAt = tournamentDto.RegistrationClosesAt, 
            StartsAt = tournamentDto.StartsAt, 
            EndsAt = tournamentDto.EndsAt,
            ParticipantsCount = tournamentDto.ParticipantsCount
        };
        
        int roundCount = (int)Math.Log2(tournamentDto.ParticipantsCount) + 1;
        tournament.Matches = [];

        Match finalMatch = new Match();
        CreateMatchGrid(tournament.Id, finalMatch, roundCount, tournament.Matches);
        SetPositions(tournament.Matches, roundCount);

        await appDbContext.Tournaments.AddAsync(tournament, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        
        return tournament.Id;
    }
}