﻿namespace eshop.catalog.api.Products.GetProducts;

public record GetProductsRequest(int? PageNumber=1, int? PageSize= Cnstnts.FetchPageSize);
public record  GetProductsResponse(IEnumerable<Product> Products, int? PageNumber = 1, int? PageSize = Cnstnts.FetchPageSize);
public class GetProductsEndPoints() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app )
    {
      
        app.MapGet(Router. ProductRootPath ,
            async ([AsParameters] GetProductsRequest request,  ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .WithSummary("Get all products")
            .WithDescription("This endpoint retrieves all products from the catalog.")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
