using MediatR;

namespace Application.BusinessLogic.Auth.Commands;

public record LogoutCommand(string? RefreshToken) : IRequest;