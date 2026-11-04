namespace Application.BusinessLogic.TeamMembers.Dtos;

public record TeamMemberDto(
    Guid TeamId,
    string TeamName,
    string MemberName,
    string Email);