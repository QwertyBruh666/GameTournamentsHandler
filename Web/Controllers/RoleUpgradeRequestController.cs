using Application.BusinessLogic.RoleUpgradeRequests.Commands;
using Application.BusinessLogic.RoleUpgradeRequests.Queries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/role-requests")]
public class RoleUpgradeRequestController(ISender sender) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRequests()
    {
        return Ok(await sender.Send(new GetRoleUpgradeRequestsQuery()));
    }

    [Authorize]
    [HttpPut("{requestId}/status/approve")]
    public async Task<IActionResult> ApproveRequest(Guid requestId)
    {
        await sender.Send(new ApproveRoleUpgradeCommand(requestId));
        return Ok();
    }

    [Authorize]
    [HttpPut("{requestId}/status/reject")]
    public async Task<IActionResult> RejectRequest(Guid requestId)
    {
        await sender.Send(new RejectRoleUpgradeCommand(requestId));
        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostRequest([FromBody] UserRole newRole)
    {
        await sender.Send(new RequestRoleUpgradeCommand(newRole));
        return Ok();
    }
}