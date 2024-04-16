using Cramming.Domain.QuizAggregate;

namespace Cramming.UseCases.Quizzes.Create
{
    public class CreateQuizHandler(IQuizRepository repository) : ICommandHandler<CreateQuizCommand, Result<QuizBriefDto>>
    {
        public async Task<Result<QuizBriefDto>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = new Quiz(request.Title);

            foreach (var questionDto in request.Questions)
            {
                var question = new QuizQuestion(questionDto.Statement);

                foreach (var optionDto in questionDto.Options)
                    question.AddOption(new QuizQuestionOption(optionDto.Text, optionDto.IsCorrect));

                quiz.AddQuestion(question);
            }

            var savedQuiz = await repository.AddAsync(quiz, cancellationToken);

            return new QuizBriefDto(savedQuiz.Id, savedQuiz.Title, savedQuiz.Questions.Count);
        }
    }
}
