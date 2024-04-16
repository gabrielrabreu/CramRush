using Cramming.Domain.QuizAttemptAggregate;

namespace Cramming.UnitTests.Domain.QuizAttemptAggregate
{
    public class QuizAttemptQuestionTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var statement = "Sample Statement";

            // Act
            var question = new QuizAttemptQuestion(statement);

            // Assert
            question.Statement.Should().Be(statement);
            question.Options.Should().BeEmpty();
            question.QuizAttempt.Should().BeNull();
            question.QuizAttemptId.Should().BeEmpty();
        }

        [Fact]
        public void SetQuizAttemptId_ShouldSetQuizAttemptId()
        {
            // Arrange
            var question = new QuizAttemptQuestion("Sample Statement");
            var newQuizId = Guid.NewGuid();

            // Act
            question.SetQuizAttemptId(newQuizId);

            // Assert
            question.QuizAttemptId.Should().Be(newQuizId);
        }

        [Fact]
        public void AddOption_ShouldAddOptionToList()
        {
            // Arrange
            var question = new QuizAttemptQuestion("Sample Statement");
            var option = new QuizAttemptQuestionOption("Sample Text", true);

            // Act
            question.AddOption(option);

            // Assert
            question.Options.Should().Contain(option);
        }

        [Fact]
        public void MarkAnswer_WhenCorrectOption_ShouldSetIsPendingAndIsCorrect()
        {
            // Arrange
            var question = new QuizAttemptQuestion("Sample Statement");

            var option1 = new QuizAttemptQuestionOption("Sample Text 1", true);
            question.AddOption(option1);

            var option2 = new QuizAttemptQuestionOption("Sample Text 2", false);
            question.AddOption(option2);

            // Act
            question.MarkAnswer(option1);

            // Assert
            question.IsPending.Should().BeFalse();
            question.IsCorrect.Should().BeTrue();
            
            option1.IsSelected.Should().BeTrue();
        }
    }
}
