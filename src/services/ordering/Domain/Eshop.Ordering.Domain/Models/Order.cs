

using Eshop.Ordering.Domain.Events;
using Eshop.Ordering.Domain.ValueObjects;

namespace Eshop.Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }
    public static Order Create(OrderId id,
                         CustomerId customerId, 
                         OrderName orderName, 
                         Address shipingAddress,
                         Address billingAddress,
                         OrderStatus status,
                         Payment payment)
    {
        var order = new Order {
        Id  = id,
        CustomerId = customerId,
        OrderName = orderName,
        ShippingAddress = shipingAddress,
        BillingAddress = billingAddress,
        Payment = payment,
        Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(
                         CustomerId customerId,
                         OrderName orderName,
                         Address shipingAddress,
                         Address billingAddress,
                         OrderStatus status,
                         Payment payment)
    {
        CustomerId = customerId;
        OrderName = orderName;
        ShippingAddress = shipingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = OrderStatus.Pending;
        this.AddDomainEvent(new OrderUpdatedEvent(this));

    }
    /// <summary>
    /// Add new Order Item 
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <param name="price"></param>
    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var orderItem =new OrderItem(Id,productId, quantity, price);
        _orderItems.Add(orderItem);
    }
    /// <summary>
    /// Remove Order Item 
    /// </summary>
    /// <param name="productId"></param>
    public void Remove(ProductId productId) {
    var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
        if (orderItem != null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}
