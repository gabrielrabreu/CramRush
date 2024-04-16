using Cramming.Domain.QuizAttemptAggregate;

namespace Cramming.UnitTests.Domain.QuizAttemptAggregate
{
    public class QuizAttemptTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var quizTitle = "Sample Title";

            // Act
            var quizAttempt = new QuizAttempt(quizTitle);

            // Assert
            quizAttempt.QuizTitle.Should().Be(quizTitle);
            quizAttempt.Questions.Should().BeEmpty();
        }

        [Fact]
        public void AddQuestion_ShouldAddQuestionToList()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");

            // Act
            quizAttempt.AddQuestion(quizAttemptQuestion);

            // Assert
            quizAttempt.Questions.Should().Contain(quizAttemptQuestion);
        }

        [Fact]
        public void MarkAnswer_WhenNoPendingQuestions_ShouldSetIsPendingToTrue()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");

            var quizAttemptQuestion1 = new QuizAttemptQuestion("Sample Statement 1");
            var quizAttemptQuestion1Option = new QuizAttemptQuestionOption("Sample Text 1", true);
            quizAttemptQuestion1.AddOption(quizAttemptQuestion1Option);
            quizAttempt.AddQuestion(quizAttemptQuestion1);

            var quizAttemptQuestion2 = new QuizAttemptQuestion("Sample Statement 2");
            var quizAttemptQuestion2Option = new QuizAttemptQuestionOption("Sample Text 1", true);
            quizAttemptQuestion2.AddOption(quizAttemptQuestion2Option);
            quizAttempt.AddQuestion(quizAttemptQuestion2);

            // Act
            quizAttempt.MarkAnswer(quizAttemptQuestion1, quizAttemptQuestion1Option);

            // Assert
            quizAttempt.IsPending.Should().BeTrue();

            // Act
            quizAttempt.MarkAnswer(quizAttemptQuestion2, quizAttemptQuestion2Option);

            // Assert
            quizAttempt.IsPending.Should().BeFalse();
        }
    }
}
