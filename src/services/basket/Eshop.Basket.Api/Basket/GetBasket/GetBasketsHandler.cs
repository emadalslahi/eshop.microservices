using eshop.buildingblocks.CQRS;
using Eshop.Basket.Api.Data;
using Eshop.Basket.Api.models;

namespace Eshop.Basket.Api.Basket.GetBasket;

public record GetBasketResult(ShopingCart ShopingCart);
public record GetBasketQuery(String UserName) :IQuery<GetBasketResult>;

public class GetBasketsQueryHandler(IBasketReposotry reposotry) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {

        var basket = await reposotry.GetBasket(request.UserName, cancellationToken);

        return new GetBasketResult(basket);
    }
}
