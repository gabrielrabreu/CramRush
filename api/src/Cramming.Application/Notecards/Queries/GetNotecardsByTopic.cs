using Cramming.Application.Common.Interfaces;
using Cramming.Application.Notecards.Interfaces;
using MediatR;

namespace Cramming.Application.Notecards.Queries
{
    /// <summary>
    /// Represents a query to retrieve Notecards for a specific topic.
    /// </summary>
    public record GetNotecardsByTopicQuery(Guid TopicId) : IRequest<FileComposed>
    {
        /// <summary>
        /// The ID of the topic for which to retrieve Notecards.
        /// </summary>
        public Guid TopicId { get; init; } = TopicId;
    }

    public class GetNotecardsByTopicQueryHandler(ITopicQueryRepository topicRepositoryQuery, INotecardComposer notecardComposer) : IRequestHandler<GetNotecardsByTopicQuery, FileComposed>
    {
        public async Task<FileComposed> Handle(GetNotecardsByTopicQuery request, CancellationToken cancellationToken)
        {
            var topicDetails = await topicRepositoryQuery.GetDetailsAsync(request.TopicId, cancellationToken);
            var fileComposed = notecardComposer.Compose(topicDetails!);
            return fileComposed;
        }
    }
}
