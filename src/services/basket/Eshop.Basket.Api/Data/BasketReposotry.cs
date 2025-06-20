using Eshop.Basket.Api.Exceptions;
using Eshop.Basket.Api.models;
using Marten;

namespace Eshop.Basket.Api.Data;

public class BasketReposotry(IDocumentSession session) : IBasketReposotry
{  
    public async Task<ShopingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShopingCart>(userName, cancellationToken);

        return basket is null ? throw new BasketNotFoundException(userName) :basket;
    }
    public async Task<ShopingCart> StoreBasket(ShopingCart shopingCart, CancellationToken cancellationToken = default)
    {
        session.Store(shopingCart); 
        await session.SaveChangesAsync(cancellationToken);
        return shopingCart;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShopingCart>(userName);
        await session.SaveChangesAsync();
        return true;
    }


}
