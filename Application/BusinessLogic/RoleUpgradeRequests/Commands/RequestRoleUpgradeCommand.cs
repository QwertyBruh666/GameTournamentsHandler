using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.RoleUpgradeRequests.Commands;

public record RequestRoleUpgradeCommand(UserRole NewRole) : IRequest;