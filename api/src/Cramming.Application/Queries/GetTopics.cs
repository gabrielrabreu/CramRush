using Cramming.Application.Common.Interfaces;
using MediatR;

namespace Cramming.Application.Queries
{
    public record TopicTagBriefDto(Guid Id, string Name);

    public record TopicBriefDto(Guid Id, string Name, string Description, IReadOnlyCollection<TopicTagBriefDto> Tags);

    public record GetTopicsQuery : IRequest<IPaginatedList<TopicBriefDto>>
    {
        public int PageNumber { get; init; } = 1;
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
