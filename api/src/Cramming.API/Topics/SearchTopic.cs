using Cramming.SharedKernel;
using Cramming.UseCases.Topics;
using Cramming.UseCases.Topics.Search;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cramming.API.Topics
{
    public class SearchTopic : EndpointBase
    {
        public const string Route = "/Topics";

        public static string BuildRoute(int pageNumber, int pageSize)
            => $"{Route}?{nameof(SearchTopicRequest.PageNumber)}={pageNumber}" +
                      $"&{nameof(SearchTopicRequest.PageSize)}={pageSize}";


        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(SearchTopic))
                .WithTags("Topics")
                .WithSummary("Search topics with pagination");
        }

        private async Task<Ok<PagedList<TopicBriefDto>>> HandleAsync(
            [AsParameters] SearchTopicRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new SearchTopicQuery(request.PageNumber, request.PageSize);

            var result = await mediator.Send(query, cancellationToken);

            return TypedResults.Ok(result.Value);
        }
    }
}
