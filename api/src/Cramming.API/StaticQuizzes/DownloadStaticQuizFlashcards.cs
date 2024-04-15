using Cramming.UseCases.StaticQuizzes.DownloadFlashcards;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.StaticQuizzes
{
    public class DownloadStaticQuizFlashcards : EndpointBase
    {
        public const string Route = "/StaticQuizzes/{StaticQuizId}:Download";

        public static string BuildRoute(Guid staticQuizId) => Route.Replace("{StaticQuizId}", staticQuizId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(DownloadStaticQuizFlashcards))
                .WithTags("StaticQuizzes")
                .WithSummary("Downloads Static Quiz flashcards by its ID");
        }

        private async Task<Results<FileContentHttpResult, NotFound>> HandleAsync(
            Guid staticQuizId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new DownloadStaticQuizFlashcardsQuery(staticQuizId);

            var result = await mediator.Send(query, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.File(
                result.Value!.Content,
                result.Value.Type,
                result.Value.Name);
        }
    }
}
