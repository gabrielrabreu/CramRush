using Cramming.SharedKernel;
using Cramming.UseCases.QuizAttempts.List;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cramming.API.StaticQuizzes
{
    public class ListQuizAttempts : EndpointBase
    {
        public const string Route = "/QuizAttempts";

        public static string BuildRoute(int pageNumber, int pageSize)
            => $"{Route}?{nameof(ListQuizAttemptsRequest.PageNumber)}={pageNumber}" +
                      $"&{nameof(ListQuizAttemptsRequest.PageSize)}={pageSize}";

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(ListQuizAttempts))
                .WithTags("QuizAttempts")
                .WithSummary("List Quiz Attempts with pagination");
        }

        private async Task<Ok<PagedList<QuizAttemptBriefDto>>> HandleAsync(
            [AsParameters] ListQuizAttemptsRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new ListQuizAttemptsQuery(request.PageNumber, request.PageSize);

            var result = await mediator.Send(query, cancellationToken);

            return TypedResults.Ok(result.Value);
        }
    }
}
