using Eshop.Ordering.Api;
using Eshop.Ordering.Application;
using Eshop.Ordering.Infrastructuer;

var builder = WebApplication.CreateBuilder(args);
//configure services

//--- add services to container 
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();
// configure pipeline
app.UseApiServices();
app.Run();
