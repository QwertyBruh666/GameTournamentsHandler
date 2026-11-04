using Application.BusinessLogic.Auth.Dtos;
using MediatR;

namespace Application.BusinessLogic.Auth.Commands;

public record LoginCommand(UserCredentialsDto Credentials) : IRequest<Dictionary<string, string>>;