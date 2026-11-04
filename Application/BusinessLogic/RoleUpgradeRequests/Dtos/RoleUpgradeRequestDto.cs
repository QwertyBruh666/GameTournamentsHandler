using Domain.Enums;

namespace Application.BusinessLogic.RoleUpgradeRequests.Dtos;

public record RoleUpgradeRequestDto(
    Guid RequesterId, 
    UserRole NewRole);