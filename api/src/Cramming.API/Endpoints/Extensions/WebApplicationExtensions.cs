using Cramming.API.Endpoints.Extensions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebApplicationExtensions
    {
        public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
        {
            string groupName = group.GetType().Name;

            return app
                .MapGroup($"/api/{groupName}")
                .WithOpenApi();
        }

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            var endpointGroupType = typeof(EndpointGroupBase);

            var assembly = Assembly.GetExecutingAssembly();

            var endpointGroupTypes = assembly.GetExportedTypes()
                .Where(x => x.IsSubclassOf(endpointGroupType));

            foreach (var type in endpointGroupTypes)
            {
                if (Activator.CreateInstance(type) is EndpointGroupBase instance)
                {
                    instance.Map(app);
                }
            }

            return app;
        }
    }
}
