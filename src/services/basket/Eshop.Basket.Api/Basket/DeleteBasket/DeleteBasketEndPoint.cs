using Carter;
using Eshop.Basket.Api.models;
using Mapster;
using MediatR;

namespace Eshop.Basket.Api.Basket.DeleteBasket;




//public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool IsSuccess );
public class DeleteBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{UserName}", async (string UserName, ISender sender) =>
        {
            var result = sender.Send(new DeleteBasketCommand(UserName));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        }).Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("Delete Basket")
        .WithDescription(" save basket")
        .WithSummary(" Delete Basket For User Name");

    }
}
