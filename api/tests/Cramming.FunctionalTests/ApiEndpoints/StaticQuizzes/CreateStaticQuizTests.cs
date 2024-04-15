using Cramming.API.StaticQuizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.StaticQuizzes;

namespace Cramming.FunctionalTests.ApiEndpoints.StaticQuizzes
{
    public class CreateStaticQuizTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsCreatedGivenValidRequest()
        {
            var route = CreateStaticQuiz.Route;
            var request = new CreateStaticQuizRequest
            {
                Title = "Sample Title",
                Questions =
                [
                    new CreateStaticQuizQuestionRequest
                    {
                        Statement = "Sample First Statement",
                        Options = []
                    },
                    new CreateStaticQuizQuestionRequest
                    {
                        Statement = "Sample Second Statement",
                        Options = [
                            new CreateStaticQuizQuestionOptionRequest {
                                Text = "Sample Text",
                                IsCorrect = true
                            },
                            new CreateStaticQuizQuestionOptionRequest {
                                Text = "Sample Text",
                                IsCorrect = false
                            }
                       ]
                    }
                ]
            };

            var response = await _client.ExecutePostAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureCreated();

            var result = await response.DeserializeAsync<StaticQuizBriefDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Title.Should().Be(request.Title);
            result.TotalQuestions.Should().Be(request.Questions.Count());
            response.EnsureLocation(GetStaticQuiz.BuildRoute(result.Id), _output);
        }
    }
}
