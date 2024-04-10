using Cramming.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Topics.Commands
{
    /// <summary>
    /// Represents a command to update a tag from a topic.
    /// </summary>
    public record UpdateTagCommand(Guid TopicId, Guid TagId, string TagName, string? TagColour) : IRequest
    {
        /// <summary>
        /// The ID of the topic from which the tag is associated.
        /// </summary>
        public Guid TopicId { get; init; } = TopicId;

        /// <summary>
        /// The ID of the tag to be updated.
        /// </summary>
        public Guid TagId { get; init; } = TagId;

        /// <summary>
        /// The updated name of the tag.
        /// </summary>
        public string TagName { get; init; } = TagName;

        /// <summary>
        /// The updated colour of the tag.
        /// </summary>
        public string? TagColour { get; init; } = TagColour;
    }

    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagCommandValidator()
        {
            RuleFor(e => e.TopicId).NotEmpty();
            RuleFor(e => e.TagId).NotEmpty();
            RuleFor(e => e.TagName).NotEmpty();
        }
    }

    public class UpdateTagCommandHandler(ITopicRepository topicRepository) : IRequestHandler<UpdateTagCommand>
    {
        public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var topic = await topicRepository.GetByIdAsync(request.TopicId, cancellationToken);

            topic!.UpdateTag(request.TagId, request.TagName, request.TagColour);

            await topicRepository.UpdateAsync(topic, cancellationToken);
            await topicRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
