using Cramming.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Topics.Commands
{
    /// <summary>
    /// Represents a command to delete a topic.
    /// </summary>
    public record DeleteTopicCommand(Guid Id) : IRequest
    {
        /// <summary>
        /// The ID of the topic to be deleted.
        /// </summary>
        public Guid Id { get; init; } = Id;
    }

    public class DeleteTopicCommandValidator : AbstractValidator<DeleteTopicCommand>
    {
        public DeleteTopicCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }

    public class DeleteTopicCommandHandler(ITopicRepository topicRepository) : IRequestHandler<DeleteTopicCommand>
    {
        public async Task Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            await topicRepository.DeleteAsync(request.Id, cancellationToken);
            await topicRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
