using Domain.Entities;

namespace Application.BusinessLogic.Access;

public record AccessEntityDto<T>(T? Entity, bool IsEnoughRights) where T : BaseEntity;