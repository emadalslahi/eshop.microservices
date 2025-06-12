using eshop.catalog.api.Models;

namespace eshop.catalog.api.Products.GetProducts;


public record class GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndPoints() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        try {
        app.MapGet("/products",
            async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .WithSummary("Get all products")
            .WithDescription("This endpoint retrieves all products from the catalog.")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
        catch (Exception ex) {
         //   logger.LogError(ex, "Error occurred while adding GetProducts endpoint routes.");
        }
    }
}
