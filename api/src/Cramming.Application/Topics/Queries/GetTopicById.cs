using Cramming.Application.Common.Interfaces;
using Cramming.Domain.Enums;
using MediatR;

namespace Cramming.Application.Topics.Queries
{
    /// <summary>
    /// Represents a detailed view of a tag associated with a topic.
    /// </summary>
    public record TopicDetailTagDto(Guid Id, string Name, string Colour)
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
    }

    /// <summary>
    /// Represents a detailed view of a question associated with a topic.
    /// </summary>
    public record TopicDetailQuestionDto(Guid Id, QuestionType Type, string Statement, string? Answer, IReadOnlyCollection<TopicDetailMultipleChoiceOptionDto>? Choices)
    {
        /// <summary>
        /// The ID of the question.
        /// </summary>
        public Guid Id { get; init; } = Id;

        /// <summary>
        /// The type of the question.
        /// </summary>
        public QuestionType Type { get; init; } = Type;

        /// <summary>
        /// The statement of the question.
        /// </summary>
        public string Statement { get; init; } = Statement;

        /// <summary>
        /// The answer to the question, if applicable.
        /// </summary>
        public string? Answer { get; init; } = Answer;

        /// <summary>
        /// The collection of options for multiple-choice questions, if applicable.
        /// </summary>
        public IReadOnlyCollection<TopicDetailMultipleChoiceOptionDto>? Options { get; init; } = Choices;
    }

    /// <summary>
    /// Represents a detailed view of a option for a multiple-choice question associated with a topic.
    /// </summary>
    public record TopicDetailMultipleChoiceOptionDto(Guid Id, string Statement, bool IsAnswer)
    {
        /// <summary>
        /// The ID of the option.
        /// </summary>
        public Guid Id { get; init; } = Id;

        /// <summary>
        /// The statement of the option.
        /// </summary>
        public string Statement { get; init; } = Statement;

        /// <summary>
        /// Indicates whether the option is the correct answer.
        /// </summary>
        public bool IsAnswer { get; init; } = IsAnswer;
    }

    /// <summary>
    /// Represents a detailed view of a topic.
    /// </summary>
    public record TopicDetailDto(Guid Id, string Name, string Description, IList<TopicDetailTagDto> Tags, IList<TopicDetailQuestionDto> Questions)
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
        /// The collection of tags associated with the topic.
        /// </summary>
        public IList<TopicDetailTagDto> Tags { get; init; } = Tags;

        /// <summary>
        /// The collection of questions associated with the topic.
        /// </summary>
        public IList<TopicDetailQuestionDto> Questions { get; init; } = Questions;
    }

    /// <summary>
    /// Represents a query to retrieve the details of a topic by its ID.
    /// </summary>
    public record GetTopicByIdQuery(Guid Id) : IRequest<TopicDetailDto?>
    {
        /// <summary>
        /// The ID of the topic to retrieve.
        /// </summary>
        public Guid Id { get; init; } = Id;
    }

    public class GetTopicByIdQueryHandler(ITopicQueryRepository topicRepositoryQuery) : IRequestHandler<GetTopicByIdQuery, TopicDetailDto?>
    {
        public async Task<TopicDetailDto?> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            return await topicRepositoryQuery.GetDetailsAsync(request.Id, cancellationToken);
        }
    }
}
