using Cramming.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Topics.Commands
{
    /// <summary>
    /// Represents a command to disassociate a tag from a topic.
    /// </summary>
    public record DisassociateTagCommand(Guid TopicId, Guid TagId) : IRequest
    {
        /// <summary>
        /// The ID of the topic from which the tag is to be disassociated.
        /// </summary>
        public Guid TopicId { get; init; } = TopicId;

        /// <summary>
        /// The ID of the tag to be disassociated from the topic.
        /// </summary>
        public Guid TagId { get; init; } = TagId;
    }

    public class DisassociateTagCommandValidator : AbstractValidator<DisassociateTagCommand>
    {
        public DisassociateTagCommandValidator()
        {
            RuleFor(e => e.TopicId).NotEmpty();
            RuleFor(e => e.TagId).NotEmpty();
        }
    }

    public class DisassociateTagCommandHandler(ITopicRepository topicRepository) : IRequestHandler<DisassociateTagCommand>
    {
        public async Task Handle(DisassociateTagCommand request, CancellationToken cancellationToken)
        {
            var topic = await topicRepository.GetByIdAsync(request.TopicId, cancellationToken);

            topic!.DisassociateTag(request.TagId);

            await topicRepository.UpdateAsync(topic, cancellationToken);
            await topicRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
