using Marten.Linq.QueryHandlers;

namespace eshop.catalog.api.Products.GetProductByCategory;



public record GetProductByCategoryQuery(string category) :IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult (IEnumerable<Product> Products);

internal class GetProductByCategoryHandler(IDocumentSession session , ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery qry, CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing Getting Product by Category Name");
        
        var products = await session.Query<Product>()
                            .Where(p=>p.Category.Contains(qry.category)).ToListAsync(cancellationToken);

        if (products is null) {
            return new GetProductByCategoryResult(new  List<Product>());
        }

        return new GetProductByCategoryResult(products);
    }
}
