using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.ReplaceQuestions
{
    public class ReplaceQuestionsHandler(ITopicRepository repository) : ICommandHandler<ReplaceQuestionsCommand, Result>
    {
        public async Task<Result> Handle(ReplaceQuestionsCommand request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            topic.ClearQuestions();

            foreach (var openEndedQuestion in request.Questions.Where(question => question.Type == QuestionType.OpenEnded))
                topic.AddOpenEndedQuestion(openEndedQuestion.Statement, openEndedQuestion.Answer!);

            foreach (var multipleChoiceQuestion in request.Questions.Where(question => question.Type == QuestionType.MultipleChoice))
            {
                var newMultipleChoiceQuestion = topic.AddMultipleChoiceQuestion(multipleChoiceQuestion.Statement);

                foreach (var multipleChoiceQuestionOption in multipleChoiceQuestion.Options)
                    newMultipleChoiceQuestion.AddOption(multipleChoiceQuestionOption.Statement, multipleChoiceQuestionOption.IsAnswer);
            }

            await repository.UpdateAsync(topic, cancellationToken);

            return Result.OK();
        }
    }
}
