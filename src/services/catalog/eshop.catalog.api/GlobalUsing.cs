global using Carter;
global using Mapster;
global using MediatR;
global using Marten;
global using eshop.buildingblocks.CQRS;
global using eshop.catalog.api.Exceptions;
global using eshop.catalog.api.Models;
global using FluentValidation;



public static class Cnstnts
{
    public const int  FetchPageSize = 10;
}


public static class Router
{
    public const string HealthCheckPath = "/health";
 
    public const string ProductRootPath = "/products";
    public const string ProductByIdRootPath = "/products/{id}";
    public const string ProductCategoryRootPath = "/products/category/{category}";

}