using Cramming.UseCases.StaticQuizzes.Delete;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.StaticQuizzes
{
    public class DeleteStaticQuiz : EndpointBase
    {
        public const string Route = "/StaticQuizzes/{StaticQuizId}";

        public static string BuildRoute(Guid staticQuizId) => Route.Replace("{StaticQuizId}", staticQuizId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapDelete(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(DeleteStaticQuiz))
                .WithTags("StaticQuizzes")
                .WithSummary("Deletes a static quiz by its ID");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            Guid staticQuizId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new DeleteStaticQuizCommand(staticQuizId);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
