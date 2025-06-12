using eshop.catalog.api.Products.GetProducts;
namespace eshop.catalog.api.Products.DeleteProduct;


public record   DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
public record DeleteProductResult(bool  IsSuccessfull);

internal class DeleteProductCommandHandler(IDocumentSession session,
                                       ILogger<GetProductsQueryHandler> logger) :ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling DeleteProductCommand");

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product Deleted with ID: {ProductId}", product.Id);
        // For demonstration, we are just returning a new Guid as the product ID.
        return  new   DeleteProductResult(true);
    }
}

