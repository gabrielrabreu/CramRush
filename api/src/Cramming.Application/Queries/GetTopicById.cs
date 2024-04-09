using Cramming.Application.Common.Interfaces;
using Cramming.Domain.Enums;
using MediatR;

namespace Cramming.Application.Queries
{
    public record TopicDetailTagDto(Guid Id, string Name);

    public record TopicDetailQuestionDto(Guid Id, QuestionType type, string Statement, string? Answer, IReadOnlyCollection<TopicDetailQuestionChoicesDto>? Choices);

    public record TopicDetailQuestionChoicesDto(Guid Id, string Statement, bool IsAnswer);

    public record TopicDetailDto(Guid Id, string Name, string Description, IReadOnlyCollection<TopicDetailTagDto> Tags, IReadOnlyCollection<TopicDetailQuestionDto> Questions);

    public record GetTopicByIdQuery(Guid Id) : IRequest<TopicDetailDto?> { }

    public class GetTopicByIdQueryHandler(ITopicQueryRepository topicRepositoryQuery) : IRequestHandler<GetTopicByIdQuery, TopicDetailDto?>
    {
        public async Task<TopicDetailDto?> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            return await topicRepositoryQuery.GetDetailsAsync(request.Id, cancellationToken);
        }
    }
}
