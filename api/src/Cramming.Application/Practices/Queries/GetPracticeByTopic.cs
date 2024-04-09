using Cramming.Application.Common.Interfaces;
using Cramming.Application.Practices.Interfaces;
using MediatR;

namespace Cramming.Application.Practices.Queries
{
    /// <summary>
    /// Represents a query to retrieve Practice Test for a specific topic.
    /// </summary>
    public record GetPracticeByTopicQuery(Guid TopicId) : IRequest<FileComposed>
    {
        /// <summary>
        /// The ID of the topic for which to retrieve Practice Test.
        /// </summary>
        public Guid TopicId { get; init; } = TopicId;
    }

    public class GetPracticeByTopicQueryHandler(ITopicQueryRepository topicRepositoryQuery, IPracticeComposer practiceComposer) : IRequestHandler<GetPracticeByTopicQuery, FileComposed>
    {
        public async Task<FileComposed> Handle(GetPracticeByTopicQuery request, CancellationToken cancellationToken)
        {
            var topicDetails = await topicRepositoryQuery.GetDetailsAsync(request.TopicId, cancellationToken);
            var fileComposed = practiceComposer.Compose(topicDetails!);
            return fileComposed;
        }
    }
}
