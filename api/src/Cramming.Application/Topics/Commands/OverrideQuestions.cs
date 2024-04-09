using Cramming.Application.Common.Interfaces;
using Cramming.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Topics.Commands
{
    /// <summary>
    /// Represents a command to override the questions associated with a topic.
    /// </summary>
    public record OverrideQuestionsCommand : IRequest
    {
        /// <summary>
        /// The ID of the topic for which the questions are to be overridden.
        /// </summary>
        public Guid TopicId { get; init; } = Guid.Empty;

        /// <summary>
        /// The collection of parameters for associating questions with the topic.
        /// </summary>
        public IReadOnlyCollection<AssociateQuestionParameters> Questions { get; init; } = [];
    }

    public class OverrideQuestionsCommandValidator : AbstractValidator<OverrideQuestionsCommand>
    {
        public OverrideQuestionsCommandValidator()
        {
            RuleFor(e => e.TopicId).NotEmpty();
        }
    }

    public class OverrideQuestionsCommandHandler(ITopicRepository topicRepository) : IRequestHandler<OverrideQuestionsCommand>
    {
        public async Task Handle(OverrideQuestionsCommand request, CancellationToken cancellationToken)
        {
            var topic = await topicRepository.GetByIdAsync(request.TopicId, cancellationToken);

            topic!.ClearQuestions();

            foreach (var createQuestionValue in request.Questions)
                topic.AssociateQuestion(createQuestionValue);

            await topicRepository.UpdateAsync(topic, cancellationToken);
            await topicRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
