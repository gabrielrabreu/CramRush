using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.Infrastructure.Data;
using Cramming.Infrastructure.Data.Queries;
using Cramming.Infrastructure.Data.Repositories;
using Cramming.Infrastructure.PDF.Services;
using Cramming.SharedKernel;
using Cramming.UseCases.Topics.GetNotecards;
using Cramming.UseCases.Topics.GetPracticeTest;
using Cramming.UseCases.Topics.Search;
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

            services.AddScoped(typeof(ITopicReadRepository), typeof(TopicRepository));
            services.AddScoped(typeof(ITopicRepository), typeof(TopicRepository));

            services.AddScoped<ISearchTopicQueryService, SearchTopicQueryService>();

            services.AddScoped<INotecardsPDFService, NotecardsPDFService>();
            services.AddScoped<IPracticeTestPDFService, PracticeTestPDFService>();

            logger.LogInformation("{Project} services registered", "Infrastructure");

            return services;
        }
    }
}
