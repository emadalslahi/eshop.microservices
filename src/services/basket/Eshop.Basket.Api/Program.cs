using Carter;
using eshop.buildingblocks.Behaviours;
using eshop.buildingblocks.Exceptions.Handler;
using eshop.buildingblocks.messaging.MassTransit;
using Eshop.Basket.Api.Data;
using Eshop.Basket.Api.models;
using Eshop.Discount.Grpc.Protos;
using HealthChecks.UI.Client;
using Marten; 

var builder = WebApplication.CreateBuilder(args);


// add services
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<IBasketReposotry, BasketReposotry>();
//builder.Services.AddScoped<IBasketReposotry, CashBasketRepository>(); // cannot register interface into 2 concret classes

//--- manuly registering..
//builder.Services.AddScoped<IBasketReposotry>(provider => { 
//var basketReposotry = provider.GetRequiredService<BasketReposotry>();
//    return new CashBasketRepository(basketReposotry, provider.GetRequiredService<IDistributedCache>());
//});


// auto register with pck scurtor : Decorator pattern.
builder.Services.Decorate<IBasketReposotry, CashBasketRepository>(); 

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddMarten(optns => {
    optns.Connection(builder.Configuration.GetConnectionString("Database")!);
    optns.Schema.For<ShopingCart>().Identity(x=>x.UserName);
}).UseLightweightSessions();


//--------- Grpc Client

builder.Services.AddGrpcClient<DiscountService.DiscountServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration.GetConnectionString("DiscountGrpc")!);
});


// Async Communication Services
builder.Services.AddMessageBrocker(builder.Configuration);


//------------| Cross Cutting Concern Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
var app = builder.Build();

app.UseExceptionHandler(expsnAp =>{});
app.MapCarter();
app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
