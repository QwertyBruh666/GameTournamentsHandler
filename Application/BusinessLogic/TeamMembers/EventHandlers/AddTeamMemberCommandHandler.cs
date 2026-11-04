using Application.BusinessLogic.TeamMembers.Commands;
using Application.BusinessLogic.TeamMembers.Dtos;
using Application.BusinessLogic.Teams.Dtos;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.TeamMembers.EventHandlers;

public class AddTeamMemberCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<AddTeamMemberCommand, Guid>
{
    public async Task<Guid> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
    {
        CreateTeamMemberDto memberDto = request.Member;
        
        bool isEnoughRights = await appDbContext.TeamMembers.AnyAsync(
            tm =>
                (tm.TeamId == request.TeamId && tm.UserId == currUser.GetUserId() && tm.Role == TeamMemberRole.Capitan) ||
                appDbContext.Users.Any(u => u.Id == currUser.GetUserId() && u.UserRole == UserRole.Admin),
            cancellationToken);

        if (!isEnoughRights)
            throw new Exception("You dont have enough rights");

        TeamMember newMember = new TeamMember { TeamId = request.TeamId, UserId = memberDto.NewMemberUserId, Role = memberDto.Role };
        
        await appDbContext.TeamMembers.AddAsync(newMember, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return newMember.Id;
    }
}