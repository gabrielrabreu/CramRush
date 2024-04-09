using Cramming.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Topics.Commands
{
    /// <summary>
    /// Represents the result of associating a tag with a topic.
    /// </summary>
    public record AssociateTagResultDto(Guid TagId, Guid TopicId, string TagName)
    {
        /// <summary>
        /// The ID of the tag associated with the topic.
        /// </summary>
        public Guid TagId { get; init; } = TagId;

        /// <summary>
        /// The ID of the topic associated with the tag.
        /// </summary>
        public Guid TopicId { get; init; } = TopicId;

        /// <summary>
        /// The name of the tag.
        /// </summary>
        public string TagName { get; init; } = TagName;
    }

    /// <summary>
    /// Represents a command to associate a tag with a topic.
    /// </summary>
    public record AssociateTagCommand : IRequest<AssociateTagResultDto>
    {
        /// <summary>
        /// The ID of the topic to associate the tag with.
        /// </summary>
        public Guid TopicId { get; init; } = Guid.Empty;

        /// <summary>
        /// The name of the tag to associate with the topic.
        /// </summary>
        public string TagName { get; init; } = string.Empty;
    }

    public class AssociateTagCommandValidator : AbstractValidator<AssociateTagCommand>
    {
        public AssociateTagCommandValidator()
        {
            RuleFor(e => e.TopicId).NotEmpty();
            RuleFor(e => e.TagName).NotEmpty();
        }
    }

    public class AssociateTagCommandHandler(ITopicRepository topicRepository) : IRequestHandler<AssociateTagCommand, AssociateTagResultDto>
    {
        public async Task<AssociateTagResultDto> Handle(AssociateTagCommand request, CancellationToken cancellationToken)
        {
            var topic = await topicRepository.GetByIdAsync(request.TopicId, cancellationToken);

            var associatedTag = topic!.AssociateTag(request.TagName);

            await topicRepository.UpdateAsync(topic, cancellationToken);
            await topicRepository.SaveChangesAsync(cancellationToken);

            return new AssociateTagResultDto(associatedTag.Id, associatedTag.TopicId, associatedTag.Name);
        }
    }
}
