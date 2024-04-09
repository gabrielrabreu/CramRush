using Cramming.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Commands
{
    public record DisassociateTagCommand(Guid TopicId, Guid TagId) : IRequest
    {
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
