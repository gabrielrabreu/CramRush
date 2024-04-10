using Cramming.Application.Common.Interfaces;
using MediatR;

namespace Cramming.Application.Topics.Queries
{
    /// <summary>
    /// Represents a brief view of a tag associated with a topic.
    /// </summary>
    public record TopicTagBriefDto(Guid Id, string Name, string Colour)
    {
        /// <summary>
        /// The ID of the tag.
        /// </summary>
        public Guid Id { get; init; } = Id;

        /// <summary>
        /// The name of the tag.
        /// </summary>
        public string Name { get; init; } = Name;

        /// <summary>
        /// The colour of the tag.
        /// </summary>
        public string Colour { get; init; } = Colour;
    };

    /// <summary>
    /// Represents a brief view of a topic.
    /// </summary>
    public record TopicBriefDto(Guid Id, string Name, string Description, int AmountQuestions, IReadOnlyCollection<TopicTagBriefDto> Tags)
    {
        /// <summary>
        /// The ID of the topic.
        /// </summary>
        public Guid Id { get; init; } = Id;

        /// <summary>
        /// The name of the topic.
        /// </summary>
        public string Name { get; init; } = Name;

        /// <summary>
        /// The description of the topic.
        /// </summary>
        public string Description { get; init; } = Description;

        /// <summary>
        /// The number of questions associated with the topic.
        /// </summary>
        public int AmountQuestions { get; init; } = AmountQuestions;

        /// <summary>
        /// The collection of tags associated with the topic.
        /// </summary>
        public IReadOnlyCollection<TopicTagBriefDto> Tags { get; init; } = Tags;
    };

    /// <summary>
    /// Represents a query to retrieve paginated list of topics.
    /// </summary>
    public record GetTopicsQuery : IRequest<IPaginatedList<TopicBriefDto>>
    {
        /// <summary>
        /// The page number of the result set.
        /// </summary>
        public int PageNumber { get; init; } = 1;

        /// <summary>
        /// The size of each page in the result set.
        /// </summary>
        public int PageSize { get; init; } = 10;
    }

    public class GetTopicsQueryHandler(ITopicQueryRepository topicRepositoryQuery) : IRequestHandler<GetTopicsQuery, IPaginatedList<TopicBriefDto>>
    {
        public async Task<IPaginatedList<TopicBriefDto>> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
        {
            return await topicRepositoryQuery.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
