using eshop.buildingblocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);
var assemply = typeof(Program).Assembly;
// Add services to the container.


builder.Services.AddMediatR(cfg => { 
    cfg.RegisterServicesFromAssembly(assemply);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assemply);
builder.Services.AddCarter();
builder.Services.AddMarten(optns => {
    optns.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();



//---------------------------------------------------
var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapCarter();


app.Run();
