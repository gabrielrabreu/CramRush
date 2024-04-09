using Cramming.Application.Common.Interfaces;
using Cramming.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Cramming.Application.Commands
{
    public record CreateTopicResultDto(Guid Id, string Name, string Description);

    public record CreateTopicCommand : IRequest<CreateTopicResultDto>
    {
        public string Name { get; init; } = string.Empty;
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
