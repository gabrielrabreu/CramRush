using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Get
{
    public class GetTopicHandler(ITopicReadRepository repository) : IQueryHandler<GetTopicQuery, Result<TopicDto>>
    {
        public async Task<Result<TopicDto>> Handle(GetTopicQuery request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            return new TopicDto(
                topic.Id,
                topic.Name,
                topic.Tags.Select(tag =>
                    new TagDto(
                        tag.Id,
                        tag.TopicId,
                        tag.Name,
                        tag.Colour?.Code)),
                topic.Questions.Select(question =>
                {
                    if (question is OpenEndedQuestion openEndedQuestion)
                        return new QuestionDto(
                            openEndedQuestion.Id,
                            openEndedQuestion.TopicId,
                            QuestionType.OpenEnded,
                            openEndedQuestion.Statement,
                            openEndedQuestion.Answer,
                            []);
                    if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                        return new QuestionDto(
                            multipleChoiceQuestion.Id,
                            multipleChoiceQuestion.TopicId,
                            QuestionType.MultipleChoice,
                            multipleChoiceQuestion.Statement,
                            null,
                            multipleChoiceQuestion.Options.Select(option =>
                                new MultipleChoiceQuestionOptionDto(
                                    option.Id,
                                    option.QuestionId,
                                    option.Statement,
                                    option.IsAnswer)));

                    return null;
                }).Where(question => question != null)!);
        }
    }
}
