using Carter;
using Eshop.Basket.Api.models;
using Mapster;
using MediatR;

namespace Eshop.Basket.Api.Basket.GetBasket;



public record GetBasketResponse(ShopingCart ShopingCart);
public class GetBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            return Results.Ok(result.Adapt<GetBasketResponse>());
        }).WithName("GetBasketByUserName")
        .WithDescription("Getting User Name Basket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("");
        
    }
}
