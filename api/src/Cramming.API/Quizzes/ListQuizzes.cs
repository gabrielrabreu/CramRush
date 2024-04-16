using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.List;

namespace Cramming.API.Quizzes
{
    public class ListQuizzes : EndpointBase
    {
        public const string Route = "/Quizzes";

        public static string BuildRoute(int pageNumber, int pageSize)
            => $"{Route}?{nameof(ListQuizzesRequest.PageNumber)}={pageNumber}" +
                      $"&{nameof(ListQuizzesRequest.PageSize)}={pageSize}";

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(ListQuizzes))
                .WithTags("Quizzes")
                .WithSummary("List Quizzes with pagination");
        }

        private async Task<Ok<PagedList<QuizBriefDto>>> HandleAsync(
            [AsParameters] ListQuizzesRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new ListQuizzesQuery(request.PageNumber, request.PageSize);

            var result = await mediator.Send(query, cancellationToken);

            return TypedResults.Ok(result.Value);
        }
    }
}
