using eshop.catalog.api.Models;

namespace eshop.catalog.api.Products.GetProducts;


public record class GetProductsQuery : IQuery<GetProductsResult>;
public record class GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session,
                                       ILogger<GetProductsQueryHandler> logger) 
    :IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductsQuery");
        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        if (products == null || !products.Any())
        {
            logger.LogWarning("No products found");
            return new GetProductsResult(new List<Product>());
        }

        return  new GetProductsResult(products);
    }
} 