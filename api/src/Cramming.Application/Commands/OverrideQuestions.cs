using Cramming.Application.Common.Interfaces;
using Cramming.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Commands
{
    public record OverrideQuestionsResultDto(Guid TagId, Guid TopicId, string TagName);

    public record OverrideQuestionsCommand : IRequest
    {
        public Guid TopicId { get; init; } = Guid.Empty;
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
