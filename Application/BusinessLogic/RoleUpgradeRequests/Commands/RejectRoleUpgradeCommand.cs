using MediatR;

namespace Application.BusinessLogic.RoleUpgradeRequests.Commands;

public record RejectRoleUpgradeCommand(Guid RequestId) : IRequest;