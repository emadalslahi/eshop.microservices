
namespace Eshop.Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, 
                                          CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.id);
        if (orderId == null)
            throw new ArgumentNullException(nameof(orderId), "OrderId cannot be null.");

        var ordr = await dbContext.Orders.FindAsync([orderId],cancellationToken);
        if (ordr == null)
            throw new OrderNotFoundException(orderId.Value);

        dbContext.Orders.Remove(ordr);
        if (await dbContext.SaveChangesAsync(cancellationToken) > 0)
        {
            return new DeleteOrderResult(true);
        }
        return new DeleteOrderResult(false);
    }
}
