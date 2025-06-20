var builder = WebApplication.CreateBuilder(args);
//configure services


var app = builder.Build();
// configure pipeline

app.Run();
