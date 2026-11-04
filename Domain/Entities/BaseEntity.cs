using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Timestamp]
    public uint RowVersion { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}