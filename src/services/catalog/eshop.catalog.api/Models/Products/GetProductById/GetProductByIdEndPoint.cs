
using eshop.catalog.api.Products.GetProductById;
using eshop.catalog.api.Products.GetProducts;

namespace eshop.catalog.api.Models.Products.GetProductById;

//public record GetProductByIdRequest();

public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}",async (Guid id , ISender sender) => {
        
            var result = await sender.Send(new GetProductByIdQuery(id));
            return Results.Ok(result.Adapt<GetProductByIdResponse>());
        }).WithName("GetProductWithId")
            .WithSummary("Get single  product by its id ")
            .WithDescription("This endpoint retrieves  product by id from the catalog.")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);

    }
}
