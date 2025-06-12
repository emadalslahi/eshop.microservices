namespace eshop.catalog.api.Products.GetProductById;
public record GetProductByIdQuery(Guid Id) :IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
internal class GetProductByIdQueryHandler(IDocumentSession session , ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        try {
            
            logger.LogInformation("Processing Qury for Getting By id Product");
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (product == null) {
                throw new ProductNotFoundException(query.Id);
            }
            return new GetProductByIdResult ( product );
        
        } catch (Exception ex){

            logger.LogError(ex, $"Error While Processing Get Product with id {query.Id}");
            throw;
        }
    }
}