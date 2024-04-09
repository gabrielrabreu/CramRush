using Cramming.Application.Common.Interfaces;
using Cramming.Infrastructure.Data.Context;
using Cramming.Infrastructure.Data.QueryRepositories;
using Cramming.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("name=ConnectionStrings:Sqlite"));

            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ITopicQueryRepository, TopicQueryRepository>();

            return services;
        }
    }
}
