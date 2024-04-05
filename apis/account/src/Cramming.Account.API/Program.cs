using Cramming.Account.API;
using Cramming.Account.API.Infrastructure;
using Cramming.Account.Application;
using Cramming.Account.Infrastructure;
using Cramming.Account.Infrastructure.Data;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add logging to the host
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();


var app = builder.Build();

app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>()!.Value);

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseSerilogRequestLogging();

app.UseHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.UseMiddleware<CustomExceptionMiddleware>();

app.Run();

public partial class Program { }

