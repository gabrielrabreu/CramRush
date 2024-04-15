using Cramming.SharedKernel;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.List;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cramming.API.StaticQuizzes
{
    public class ListStaticQuizzes : EndpointBase
    {
        public const string Route = "/StaticQuizzes";

        public static string BuildRoute(int pageNumber, int pageSize)
            => $"{Route}?{nameof(ListStaticQuizzesRequest.PageNumber)}={pageNumber}" +
                      $"&{nameof(ListStaticQuizzesRequest.PageSize)}={pageSize}";


        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(ListStaticQuizzes))
                .WithTags("StaticQuizzes")
                .WithSummary("List Static Quizzes with pagination");
        }

        private async Task<Ok<PagedList<StaticQuizBriefDto>>> HandleAsync(
            [AsParameters] ListStaticQuizzesRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new ListStaticQuizzesQuery(request.PageNumber, request.PageSize);

            var result = await mediator.Send(query, cancellationToken);

            return TypedResults.Ok(result.Value);
        }
    }
}
