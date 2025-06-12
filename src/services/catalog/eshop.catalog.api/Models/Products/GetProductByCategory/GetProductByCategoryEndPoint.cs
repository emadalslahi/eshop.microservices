
using eshop.catalog.api.Products.GetProducts;

namespace eshop.catalog.api.Products.GetProductByCategory;


// Request
public record GetProductByCategoryResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",async (string category , ISender sender) =>
        {

            var result = await sender.Send(new GetProductByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        }).WithName("GetProductWithCategory")
          .WithSummary("Get  products with category")
            .WithDescription("This endpoint retrieves all products from the catalog for some category")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
