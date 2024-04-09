using Cramming.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Commands
{
    public record AssociateTagResultDto(Guid TagId, Guid TopicId, string TagName);

    public record AssociateTagCommand : IRequest<AssociateTagResultDto>
    {
        public Guid TopicId { get; init; } = Guid.Empty;
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
