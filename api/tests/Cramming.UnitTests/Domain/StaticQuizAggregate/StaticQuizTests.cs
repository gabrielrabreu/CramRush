using Cramming.Domain.StaticQuizAggregate;

namespace Cramming.UnitTests.Domain.StaticQuizAggregate
{
    public class StaticQuizTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var name = "Sample Title";

            // Act
            var quiz = new StaticQuiz(name);

            // Assert
            quiz.Title.Should().Be(name);
            quiz.Questions.Should().BeEmpty();
        }

        [Fact]
        public void SetTitle_ShouldUpdateTitle()
        {
            // Arrange
            var quiz = new StaticQuiz("Old Title");
            var newTitle = "New Title";

            // Act
            quiz.SetTitle(newTitle);

            // Assert
            quiz.Title.Should().Be(newTitle);
        }

        [Fact]
        public void AddQuestion_ShouldAddQuestionToList()
        {
            // Arrange
            var quiz = new StaticQuiz("Sample Title");
            var question = new StaticQuizQuestion("Sample Statement");

            // Act
            quiz.AddQuestion(question);

            // Assert
            quiz.Questions.Should().Contain(question);
        }

        [Fact]
        public void ClearQuestions_ShouldRemoveAllQuestionsFromList()
        {
            // Arrange
            var quiz = new StaticQuiz("Sample Title");
            quiz.AddQuestion(new StaticQuizQuestion("Sample Statement"));

            // Act
            quiz.ClearQuestions();

            // Assert
            quiz.Questions.Should().BeEmpty();
        }
    }
}
