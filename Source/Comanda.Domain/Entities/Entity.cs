namespace Comanda.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }

    public void MarkAsDeleted() => IsDeleted = true;
    public void MarkAsNotDeleted() => IsDeleted = false;
    public void MarkAsUpdatedNow() => UpdatedAt = DateTime.Now;
}