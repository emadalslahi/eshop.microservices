using eshop.catalog.api.Products.CreateProduct;

namespace eshop.catalog.api.Products.UpdateProduct;
public record  UpdateProductRequest(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageFileName,
    List<string> Category
);
public record  UpdateProductResponse(Product Product);
public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapPut("/products",
            async (UpdateProductRequest rqst, ISender sender) =>
            {
                var command = rqst.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);

            }).WithName("UpdateProduct")
            .WithSummary("Update a new product")
            .WithDescription("This endpoint allows you to Update a new product in the catalog.")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
