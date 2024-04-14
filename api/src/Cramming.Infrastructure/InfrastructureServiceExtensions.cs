using Cramming.Infrastructure.Data;
using Cramming.Infrastructure.Data.Queries;
using Cramming.Infrastructure.Data.Repositories;
using Cramming.SharedKernel;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.List;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cramming.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            ILogger logger)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("name=ConnectionStrings:SqliteConnection"));

            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped(typeof(IStaticQuizReadRepository), typeof(StaticQuizRepository));
            services.AddScoped(typeof(IStaticQuizRepository), typeof(StaticQuizRepository));

            services.AddScoped<IListStaticQuizzesService, ListStaticQuizzesService>();

            logger.LogInformation("{Project} services registered", "Infrastructure");

            return services;
        }
    }
}
