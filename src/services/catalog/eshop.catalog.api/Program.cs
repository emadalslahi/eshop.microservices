using eshop.buildingblocks.Behaviours;
using eshop.buildingblocks.Exceptions.Handler;
using eshop.catalog.api.Data;
using HealthChecks.UI.Client;


var builder = WebApplication.CreateBuilder(args);
var assemply = typeof(Program).Assembly;
// Add services to the container.


builder.Services.AddMediatR(cfg => { 
    cfg.RegisterServicesFromAssembly(assemply);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assemply);
builder.Services.AddCarter();
builder.Services.AddMarten(optns => {
    optns.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialSeedData>();
}

builder.Services.AddHealthChecks().AddNpgSql(connectionString: builder.Configuration.GetConnectionString("Database")!);
//---------------------------------------------------
var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseExceptionHandler(options => { });

/*
app.UseExceptionHandler(xpsnApp => {

    xpsnApp.Run(async context => {
        var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("Error Handling");
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is null) { return; }
        
        var problemDetails = new ProblemDetails 
        {
        Title = "UnExpected Error While Processing The Request.!",
        Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Extensions = {
                    {"traceId",Activity.Current?.TraceId}
                }
        };
        logger.LogError(exception, "Could not process a request on machin {machinID}. Trace ID: {traceId}",
                  Environment.MachineName, Activity.Current?.TraceId);

        var env = context.RequestServices.GetRequiredService<IHostEnvironment>();
        //Detail = exception.StackTrace,
        if (env.IsDevelopment())
        {
            problemDetails.Extensions.Add("stackTrace", exception?.StackTrace);
        }

        logger.LogError(exception,exception?.Message);
        context.Response.StatusCode =StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    });

});
*/
app.MapCarter();
app.UseHealthChecks(Router.HealthCheckPath, new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions{ 
ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
