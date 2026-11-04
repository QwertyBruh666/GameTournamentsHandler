using MediatR;

namespace Application.BusinessLogic.Auth.Commands;

public record RefreshTokensCommand(string? RefreshToken) :  IRequest<Dictionary<string, string>>;