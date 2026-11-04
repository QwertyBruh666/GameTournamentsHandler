using Domain.Enums;

namespace Application.BusinessLogic.Auth.Dtos;

public record RegisterDto(UserCredentialsDto Credentials, UserRole Role, string AdminKey);