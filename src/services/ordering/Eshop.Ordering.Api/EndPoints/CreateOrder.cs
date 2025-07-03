using Carter;
using Eshop.Ordering.Application.Dtos;
using Eshop.Ordering.Application.Orders.Commands.CreateOrder;
using Mapster;
using MediatR;

namespace Eshop.Ordering.Api.EndPoints;

public record CreateOrderRequest( OrderDto Order);
public record CreateOrderResponse(Guid Id);
public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

           app.MapPost("/orders", 
                    async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                var result = await sender.Send(command);
                var response = new CreateOrderResponse(result.OrderId);
                return Results.Created($"/orders/{response.Id}",response);
            })
            .WithName("CreateOrder")
            .WithTags("Orders")
            .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

    }
}
