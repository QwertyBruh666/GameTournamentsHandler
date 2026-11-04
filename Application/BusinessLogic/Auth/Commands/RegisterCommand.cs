using Application.BusinessLogic.Auth.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.Auth.Commands;

public record RegisterCommand(RegisterDto RegisterInfo) : IRequest<Dictionary<string, string>>;