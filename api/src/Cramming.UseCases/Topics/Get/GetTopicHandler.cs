using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Get
{
    public class GetTopicHandler(ITopicReadRepository repository) : IQueryHandler<GetTopicQuery, Result<TopicDTO>>
    {
        public async Task<Result<TopicDTO>> Handle(GetTopicQuery request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            return new TopicDTO(
                topic.Id,
                topic.Name,
                topic.Tags.Select(tag =>
                    new TagDTO(
                        tag.Id,
                        tag.TopicId,
                        tag.Name,
                        tag.Colour?.Code)),
                topic.Questions.Select(question =>
                {
                    if (question is OpenEndedQuestion openEndedQuestion)
                        return new QuestionDTO(
                            openEndedQuestion.Id,
                            openEndedQuestion.TopicId,
                            QuestionType.OpenEnded,
                            openEndedQuestion.Statement,
                            openEndedQuestion.Answer,
                            []);
                    if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                        return new QuestionDTO(
                            multipleChoiceQuestion.Id,
                            multipleChoiceQuestion.TopicId,
                            QuestionType.MultipleChoice,
                            multipleChoiceQuestion.Statement,
                            null,
                            multipleChoiceQuestion.Options.Select(option =>
                                new MultipleChoiceQuestionOptionDTO(
                                    option.Id,
                                    option.QuestionId,
                                    option.Statement,
                                    option.IsAnswer)));

                    return null;
                }).Where(question => question != null)!);
        }
    }
}
