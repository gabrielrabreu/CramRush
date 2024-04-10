using Cramming.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Topics.Commands
{
    /// <summary>
    /// Represents a command to update a topic.
    /// </summary>
    public record UpdateTopicCommand(Guid Id, string Name, string Description) : IRequest
    {
        /// <summary>
        /// The ID of the topic to be updated.
        /// </summary>
        public Guid Id { get; init; } = Id;

        /// <summary>
        /// The updated name of the topic.
        /// </summary>
        public string Name { get; init; } = Name;

        /// <summary>
        /// The updated description of the topic.
        /// </summary>
        public string Description { get; init; } = Description;
    }

    public class UpdateTopicCommandValidator : AbstractValidator<UpdateTopicCommand>
    {
        public UpdateTopicCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
        }
    }

    public class UpdateTopicCommandHandler(ITopicRepository topicRepository) : IRequestHandler<UpdateTopicCommand>
    {
        public async Task Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await topicRepository.GetByIdAsync(request.Id, cancellationToken);

            topic!.Name = request.Name;
            topic.Description = request.Description;

            await topicRepository.UpdateAsync(topic, cancellationToken);
            await topicRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
