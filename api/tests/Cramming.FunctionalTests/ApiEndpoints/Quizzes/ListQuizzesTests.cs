using Cramming.API.Quizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Quizzes;

namespace Cramming.FunctionalTests.ApiEndpoints.Quizzes
{
    public class ListQuizzesTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsOk()
        {
            await EnsureExistingQuiz("Sample Title 1");
            var quiz = await EnsureExistingQuiz("Sample Title 2");
            await EnsureExistingQuiz("Sample Title 3");

            var route = ListQuizzes.BuildRoute(2, 1);
            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<StaticPagedList<QuizBriefDto>>(_output);
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1)
                .And.SatisfyRespectively(item =>
                {
                    item.Id.Should().Be(quiz.Id);
                    item.Title.Should().Be(quiz.Title);
                    item.TotalQuestions.Should().Be(1);
                });
            result.PageNumber.Should().Be(2);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(3);
            result.HasPreviousPage.Should().BeTrue();
            result.HasNextPage.Should().BeTrue();

        }

        private async Task<QuizBriefDto> EnsureExistingQuiz(string title)
        {
            var route = CreateQuiz.BuildRoute();
            var request = new CreateQuizRequest
            {
                Title = title,
                Questions =
                [
                    new CreateQuizQuestionRequest
                    {
                        Statement = "Sample Statement",
                        Options = [
                            new CreateQuizQuestionOptionRequest {
                                Text = "Sample Text",
                                IsCorrect = true
                            }
                       ]
                    }
                ]
            };

            var response = await _client.ExecutePostAsync(route, request, _output);

            return await response.DeserializeAsync<QuizBriefDto>(_output);
        }
    }
}
