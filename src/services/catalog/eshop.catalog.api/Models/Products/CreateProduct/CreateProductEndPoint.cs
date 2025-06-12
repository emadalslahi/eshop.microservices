namespace eshop.catalog.api.Products.CreateProduct;
public record class CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string ImageFileName,
    List<string> Category
);
public record class CreateProductResponse(Guid Id);
public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapPost("/products",
            async (CreateProductRequest rqst, ISender sender) =>
            {
                var command = rqst.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}", response);

            }).WithName("CreateProduct")
            .WithSummary("Create a new product")
            .WithDescription("This endpoint allows you to create a new product in the catalog.")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
