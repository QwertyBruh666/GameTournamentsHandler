using Application.BusinessLogic.Matches.Commands;
using Application.BusinessLogic.Matches.Criteria;
using Application.BusinessLogic.Matches.Dtos;
using Application.BusinessLogic.Matches.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/matches")]
public class MatchController(ISender sender) : ControllerBase
{
    [HttpPut("{matchId}/start")]
    public async Task<IActionResult> StartMatch(Guid matchId)
    {
        await sender.Send(new StartMatchCommand(matchId));
        return Ok();
    }

    [HttpPut("{matchId}/finish")]
    public async Task<IActionResult> FinishMatch(Guid matchId, [FromBody] MatchResultRequestDto matchResult)
    {
        await sender.Send(new FinishMatchCommand(matchId, matchResult));
        return Ok();
    }

    [HttpGet("{matchId}")]
    public async Task<IActionResult> GetMatch(Guid matchId)
    {
        MatchDto res = await sender.Send(new GetMatchQuery { MatchId =  matchId });
        return Ok(res);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<MatchDto>>> GetMatches([FromQuery] MatchFilter filter)
    {
        List<MatchDto> matches = await sender.Send(new GetMatchesQuery(filter));
        return Ok(matches);
    }

    [HttpDelete("{matchId}")]
    public async Task<IActionResult> DeleteMatch(Guid matchId)
    {
        await sender.Send(new RemoveMatchCommand(matchId));
        return Ok();
    }
}