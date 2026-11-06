using Application.BusinessLogic.Games.Commands;
using Application.BusinessLogic.Games.Dtos;
using Application.BusinessLogic.Games.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/games")]
public class GameController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GameDto>>> GetGames()
    {
        List<GameDto> games = await sender.Send(new GetGamesQuery()); 
        return Ok(games);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> AddGame([FromBody] CreateGameDto gameDto)
    {
        Guid gameId = await sender.Send(new CreateGameCommand(gameDto));
        return Ok(gameId);
    }

    [Authorize]
    [HttpPut("{gameId}")]
    public async Task<IActionResult> UpdateActiveGameStatus(Guid gameId, [FromBody] bool activeStatus)
    {
        await sender.Send(new UpdateActiveStatusCommand(gameId, activeStatus));
        return Ok();
    }
}