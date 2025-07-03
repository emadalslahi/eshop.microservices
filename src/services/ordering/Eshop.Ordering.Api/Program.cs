using Eshop.Ordering.Api;
using Eshop.Ordering.Application;
using Eshop.Ordering.Infrastructuer;
using Eshop.Ordering.Infrastructuer.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
//configure services

//--- add services to container 
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();
// configure pipeline
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.Run();
