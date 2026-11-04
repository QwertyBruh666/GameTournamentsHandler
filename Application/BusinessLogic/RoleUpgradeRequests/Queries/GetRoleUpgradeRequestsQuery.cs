using Application.BusinessLogic.RoleUpgradeRequests.Dtos;
using MediatR;

namespace Application.BusinessLogic.RoleUpgradeRequests.Queries;

public record GetRoleUpgradeRequestsQuery() : IRequest<List<RoleUpgradeRequestDto>>;