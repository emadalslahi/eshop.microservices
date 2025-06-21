
namespace Eshop.Ordering.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get ; set ; }
    public DateTime? CreatedAt { get ; set ; }
    public string? CreatedBy { get ; set ; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get ; set ; }
}
