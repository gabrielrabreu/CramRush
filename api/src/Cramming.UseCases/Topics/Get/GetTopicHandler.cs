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

            var tags = topic.Tags.Select(tag =>
                new TagDto(
                    tag.Id,
                    tag.TopicId,
                    tag.Name,
                    tag.Colour!.Code));

            var questions = new List<QuestionDto>();
            foreach (var question in topic.Questions)
            {
                if (question is OpenEndedQuestion openEndedQuestion)
                    questions.Add(new QuestionDto(
                        openEndedQuestion.Id,
                        openEndedQuestion.TopicId,
                        QuestionType.OpenEnded,
                        openEndedQuestion.Statement,
                        openEndedQuestion.Answer,
                        []));

                else if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                    questions.Add(new QuestionDto(
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
                                option.IsAnswer))));
            }

            return new TopicDto(
                topic.Id,
                topic.Name,
                tags,
                questions);
        }
    }
}
