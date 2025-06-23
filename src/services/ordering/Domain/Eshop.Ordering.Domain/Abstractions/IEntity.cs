namespace Eshop.Ordering.Domain.Abstractions;

public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
    public int? UpdatedCount { get; set; }
}

public interface IEntity<T> : IEntity 
{
    public T Id { get; set; }
}
