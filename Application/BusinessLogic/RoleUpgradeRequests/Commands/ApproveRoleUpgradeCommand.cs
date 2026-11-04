using MediatR;

namespace Application.BusinessLogic.RoleUpgradeRequests.Commands;

public record ApproveRoleUpgradeCommand(Guid RequestId) : IRequest;