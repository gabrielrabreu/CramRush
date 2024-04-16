using Cramming.Infrastructure.Data;
using Cramming.Infrastructure.Data.Queries;
using Cramming.Infrastructure.Data.Repositories;
using Cramming.Infrastructure.Pdf;
using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.QuizAttempts.DownloadToReply;
using Cramming.UseCases.QuizAttempts.List;
using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.DownloadFlashcards;
using Cramming.UseCases.Quizzes.List;
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
                options.UseNpgsql("name=ConnectionStrings:NpgsqlConnection"));

            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped(typeof(IQuizReadRepository), typeof(QuizRepository));
            services.AddScoped(typeof(IQuizRepository), typeof(QuizRepository));

            services.AddScoped(typeof(IQuizAttemptReadRepository), typeof(QuizAttemptRepository));
            services.AddScoped(typeof(IQuizAttemptRepository), typeof(QuizAttemptRepository));

            services.AddScoped<IListQuizzesService, ListQuizzesService>();
            services.AddScoped<IListQuizAttemptsService, ListQuizAttemptsService>();

            QuestPDF.Settings.License = LicenseType.Community;
            services.AddScoped<IQuizFlashcardsPdfService, QuizFlashcardsPdfService>();
            services.AddScoped<IQuizAttemptToReplyPdfService, QuizAttemptToReplyPdfService>();

            logger.LogInformation("{Project} services registered", "Infrastructure");

            return services;
        }
    }
}
