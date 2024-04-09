using Cramming.Application.Common.Interfaces;
using Cramming.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Topics.Commands
{
    /// <summary>
    /// Represents the result of creating a new topic.
    /// </summary>
    public record CreateTopicResultDto(Guid Id, string Name, string Description)
    {
        /// <summary>
        /// The ID of the created topic.
        /// </summary>
        public Guid Id { get; init; } = Id;

        /// <summary>
        /// The name of the created topic.
        /// </summary>
        public string Name { get; init; } = Name;

        /// <summary>
        /// The description of the created topic.
        /// </summary>
        public string Description { get; init; } = Description;
    }

    /// <summary>
    /// Represents a command to create a new topic.
    /// </summary>
    public record CreateTopicCommand : IRequest<CreateTopicResultDto>
    {
        /// <summary>
        /// The name of the topic.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// The description of the topic.
        /// </summary>
        public string Description { get; init; } = string.Empty;
    }

    public class CreateTopicCommandValidator : AbstractValidator<CreateTopicCommand>
    {
        public CreateTopicCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
        }
    }

    public class CreateTopicCommandHandler(ITopicRepository topicRepository) : IRequestHandler<CreateTopicCommand, CreateTopicResultDto>
    {
        public async Task<CreateTopicResultDto> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = new TopicEntity(request.Name!, request.Description!);

            await topicRepository.AddAsync(topic, cancellationToken);
            await topicRepository.SaveChangesAsync(cancellationToken);

            return new CreateTopicResultDto(topic.Id, topic.Name, topic.Description);
        }
    }
}
