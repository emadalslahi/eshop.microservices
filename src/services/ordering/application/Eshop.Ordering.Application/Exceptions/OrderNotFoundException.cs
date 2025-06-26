using eshop.buildingblocks.Exceptions;
namespace Eshop.Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id):base("Order", id)    {    }
}
