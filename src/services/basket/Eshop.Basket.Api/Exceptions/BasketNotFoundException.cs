using eshop.buildingblocks.Exceptions;

namespace Eshop.Basket.Api.Exceptions;

public class BasketNotFoundException :NotFoundException
{
    public BasketNotFoundException(string userName): base("Basket",userName)
    {
    }

}
