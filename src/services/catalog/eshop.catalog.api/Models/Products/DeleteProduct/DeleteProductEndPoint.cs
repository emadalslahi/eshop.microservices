namespace eshop.catalog.api.Products.DeleteProduct;
//public record   DeleteProductRequest(Guid Id);
public record   DeleteProductResponse(bool IsSuccessfull);
public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapDelete("/products/{id}",
            async (Guid id , ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);

            }).WithName("DeleteProduct")
            .WithSummary("Delete  product")
            .WithDescription("This endpoint allows you to Delete a new product in the catalog.")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
