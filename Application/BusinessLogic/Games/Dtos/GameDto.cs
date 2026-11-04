namespace Application.BusinessLogic.Games.Dtos;

public record GameDto(Guid Id, string Name, string Description, bool IsActive);