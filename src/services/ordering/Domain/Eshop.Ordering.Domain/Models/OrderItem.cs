 
namespace Eshop.Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    protected OrderItem() { }
    internal OrderItem(OrderId ordrId, ProductId prodId, int qt, decimal price)
    {
        this.Id=OrderItemId.Of(Guid.NewGuid());
        this.OrderId = ordrId;
        this.ProductId = prodId;
        this.Quantity = qt;
        this.Price = price;
    }
}
