using Carter;
using Eshop.Basket.Api.models;
using Mapster;
using MediatR;

namespace Eshop.Basket.Api.Basket.StoreBasket;




public record StoreBasketRequest(ShopingCart Cart);
public record StoreBasketResponse(bool IsSuccess , string UserName);
public class StoreBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {

            var command = request.Adapt<StoreBasketCommand>();
            var result =await sender.Send(command);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{response.UserName}", response);
        }).Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("Store Basket")
        .WithDescription(" save basket")
        .WithSummary(" Store Basket For User Name");

    }
}
