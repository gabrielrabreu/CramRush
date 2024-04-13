using Cramming.API;
using Cramming.Infrastructure;
using Cramming.Infrastructure.Data;
using Cramming.SharedKernel;
using Cramming.UseCases.Topics.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Extensions.Logging;
using System.Reflection;
using System.Text.Json.Serialization;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSwaggerGen(options =>
{
    foreach (var file in Directory.GetFiles(
        AppContext.BaseDirectory,
        "*.xml",
        SearchOption.AllDirectories))
    {
        options.IncludeXmlComments(filePath: file);
    }

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cramming API",
        Version = "v1"
    });
});

ConfigureMediatR(builder.Services);

builder.Services.AddInfrastructureServices(microsoftLogger);

var app = builder.Build();

ConfigureEndpoints(app);

app.UseSwagger();
app.UseSwaggerUI();

SeedDatabase(app);

app.Run();

static void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {ExceptionMessage}", ex.Message);
    }
}

static void ConfigureMediatR(IServiceCollection services)
{
    var mediatRAssemblies = new[]
    {
        Assembly.GetAssembly(typeof(GetTopicQuery)) // UseCases
    };

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
}

static void ConfigureEndpoints(WebApplication app)
{
    var endpointBaseType = typeof(EndpointBase);

    var assembly = Assembly.GetExecutingAssembly();

    var endpointBaseTypes = assembly.GetExportedTypes()
        .Where(p => p.IsSubclassOf(endpointBaseType));

    foreach (var type in endpointBaseTypes)
        if (Activator.CreateInstance(type) is EndpointBase instance)
            instance.Configure(app);
}

public partial class Program 
{ 
    protected Program()
    {
    }
}