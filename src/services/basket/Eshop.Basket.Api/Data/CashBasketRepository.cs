using Eshop.Basket.Api.models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Eshop.Basket.Api.Data;

public class CashBasketRepository(IBasketReposotry reposotry,
                                  IDistributedCache cache) : IBasketReposotry
{

    public async Task<ShopingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {

        var cashedBasket = await cache.GetStringAsync(userName,cancellationToken);

        if (!string.IsNullOrEmpty(cashedBasket))
            return   JsonSerializer.Deserialize<ShopingCart>(cashedBasket)!;
       
        var basket = await reposotry.GetBasket(userName, cancellationToken);

        await cache.SetStringAsync(userName, JsonSerializer.Serialize<ShopingCart>(basket));
        return basket;
    }

    public async Task<ShopingCart> StoreBasket(ShopingCart shopingCart, CancellationToken cancellationToken = default)
    {
      var basket =   await reposotry.StoreBasket(shopingCart, cancellationToken);
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize<ShopingCart>(basket));
        return basket;

    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await reposotry.DeleteBasket(userName, cancellationToken);
        await cache.RemoveAsync(userName);
        return true;
    }

}
