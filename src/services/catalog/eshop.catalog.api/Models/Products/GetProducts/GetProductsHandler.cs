using Marten.Pagination;

namespace eshop.catalog.api.Products.GetProducts;


public record class GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductsResult>;
public record class GetProductsResult(IEnumerable<Product> Products, int? PageNumber = 1, int? PageSize = Cnstnts.FetchPageSize);

internal class GetProductsQueryHandler(IDocumentSession session,
                                       ILogger<GetProductsQueryHandler> logger) 
    :IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductsQuery");

        //var products = await session.Query<Product>().ToListAsync(cancellationToken);
        var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber??1,query.PageSize?? Cnstnts.FetchPageSize, cancellationToken);

        if (products == null || !products.Any())
        {
            logger.LogWarning("No products found");
            return new GetProductsResult(new List<Product>());
        }

        return  new GetProductsResult(products,query.PageNumber,query.PageSize);
    }
} 