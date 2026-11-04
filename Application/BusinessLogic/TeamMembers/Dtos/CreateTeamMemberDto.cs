using Domain.Enums;

namespace Application.BusinessLogic.TeamMembers.Dtos;

public record CreateTeamMemberDto(Guid NewMemberUserId, TeamMemberRole Role);