
var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices();

builder.Services.AddWebServices();

var app = builder.Build();

app.UseCustomLogging();

app.UseCustomLocalization();

await app.InitialiseDatabaseAsync();

app.UseCustomExceptionHandler();

app.UseCustomHealthChecks();

app.UseCustomSwagger();

app.MapEndpoints();

app.Run();
