namespace eshop.buildingblocks.messaging.Events;

public record IntegerationEvent
{
    public Guid Id =>Guid.NewGuid();
    public DateTime CreatedAt =>DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
