using Application.BusinessLogic.TournamentsMembers.Commands;
using Application.BusinessLogic.TournamentsMembers.Dtos;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/tournament-members")]
public class TournamentMembersController(ISender sender) : ControllerBase  
{
    [HttpPut("{tournamentMemberId}")]
    public async Task<IActionResult> UpdateTournamentMemberStatus(Guid tournamentMemberId, [FromBody] TournamentMemberStatus newStatus)
    {
        await sender.Send(new UpdateTournamentMemberStatusCommand(tournamentMemberId, newStatus));
        return Ok();
    }
}