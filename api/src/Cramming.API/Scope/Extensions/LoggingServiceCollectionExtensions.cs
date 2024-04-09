using Serilog;
using Serilog.Events;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggingServiceCollectionExtensions
    {
        public static void AddCustomLogging(this IHostBuilder host)
        {
            host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .ReadFrom.Configuration(ctx.Configuration));
        }

        public static void UseCustomLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();
        }
    }
}
