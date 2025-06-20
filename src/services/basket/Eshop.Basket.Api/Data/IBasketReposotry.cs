using Eshop.Basket.Api.models;

namespace Eshop.Basket.Api.Data;

public interface IBasketReposotry
{
    Task<ShopingCart> GetBasket(string userName, CancellationToken cancellationToken = default);
    Task<ShopingCart> StoreBasket(ShopingCart shopingCart, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
