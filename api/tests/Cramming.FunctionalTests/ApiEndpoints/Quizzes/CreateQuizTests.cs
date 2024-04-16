using Cramming.API.Quizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Quizzes;

namespace Cramming.FunctionalTests.ApiEndpoints.Quizzes
{
    public class CreateQuizTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsCreatedGivenValidRequest()
        {
            var route = CreateQuiz.BuildRoute();
            var request = new CreateQuizRequest
            {
                Title = "Sample Title",
                Questions =
                [
                    new CreateQuizQuestionRequest
                    {
                        Statement = "Sample First Statement",
                        Options = []
                    },
                    new CreateQuizQuestionRequest
                    {
                        Statement = "Sample Second Statement",
                        Options = [
                            new CreateQuizQuestionOptionRequest {
                                Text = "Sample Text",
                                IsCorrect = true
                            },
                            new CreateQuizQuestionOptionRequest {
                                Text = "Sample Text",
                                IsCorrect = false
                            }
                       ]
                    }
                ]
            };

            var response = await _client.ExecutePostAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureCreated();

            var result = await response.DeserializeAsync<QuizBriefDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Title.Should().Be(request.Title);
            result.TotalQuestions.Should().Be(request.Questions.Count());
            response.EnsureLocation(GetQuiz.BuildRoute(result.Id), _output);
        }
    }
}
