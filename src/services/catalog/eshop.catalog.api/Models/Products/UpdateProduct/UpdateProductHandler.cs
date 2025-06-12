

namespace eshop.catalog.api.Products.CreateProduct;
public record   UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageFileName,
    List<string> Category
):ICommand<UpdateProductResult>;
public record UpdateProductResult(Product Product);

internal class UpdateProductCommandHandler(IDocumentSession session,
                                       ILogger<UpdateProductCommandHandler> logger) 
    :ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Product Command ");


        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        
        if (product is null)
            throw new ProductNotFoundException();


        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFileName = command.ImageFileName;
        product.Category = command.Category;
        

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product Updated for id: {ProductId}", product.Id);
        // For demonstration, we are just returning a new Guid as the product ID.
        return  new UpdateProductResult(product);
    }
}

