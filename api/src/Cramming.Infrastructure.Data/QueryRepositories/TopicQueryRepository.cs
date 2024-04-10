using Cramming.Application.Common.Interfaces;
using Cramming.Application.Topics.Queries;
using Cramming.Domain.Enums;
using Cramming.Infrastructure.Data.Common;
using Cramming.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cramming.Infrastructure.Data.QueryRepositories
{
    public class TopicQueryRepository(ApplicationDbContext context) : ITopicQueryRepository
    {
        public async Task<TopicDetailDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken)
        {
            var data = await context.Topics.FindAsync([id], cancellationToken);

            if (data == null) return null;

            await context.Entry(data).Collection(p => p.Tags).LoadAsync(cancellationToken);
            var tags = new List<TopicDetailTagDto>();
            foreach (var tag in data.Tags)
                tags.Add(new TopicDetailTagDto(tag.Id, tag.Name, tag.Colour));

            await context.Entry(data).Collection(p => p.Questions).LoadAsync(cancellationToken);
            var questions = new List<TopicDetailQuestionDto>();
            foreach (var question in data.Questions)
            {
                if (question is TopicOpenEndedQuestionData openEndedQuestion)
                    questions.Add(new TopicDetailQuestionDto(
                        openEndedQuestion.Id,
                        QuestionType.OpenEnded,
                        openEndedQuestion.Statement,
                        openEndedQuestion.Answer,
                        null));

                else if (question is TopicMultipleChoiceQuestionData multipleChoiceQuestion)
                {
                    await context.Entry(multipleChoiceQuestion).Collection(p => p.Options).LoadAsync(cancellationToken);
                    questions.Add(new TopicDetailQuestionDto(
                        multipleChoiceQuestion.Id,
                        QuestionType.MultipleChoice,
                        multipleChoiceQuestion.Statement,
                        null,
                        multipleChoiceQuestion.Options.Select(option =>
                            new TopicDetailMultipleChoiceOptionDto(
                                option.Id,
                                option.Statement,
                                option.IsAnswer)).ToList()));
                }
            }

            return new TopicDetailDto(data.Id, data.Name, data.Description, tags, questions);
        }

        public async Task<IPaginatedList<TopicBriefDto>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await context.Topics
                .Include(n => n.Tags)
                .Include(n => n.Questions)
                .OrderByDescending(k => k.CreatedOn)
                .Select(s1 => new TopicBriefDto(
                    s1.Id,
                    s1.Name,
                    s1.Description,
                    s1.Questions.Count,
                    s1.Tags.Select(s2 =>
                    new TopicTagBriefDto(
                        s2.Id,
                        s2.Name,
                        s2.Colour)).ToList()))
                .PaginatedListAsync(pageNumber, pageSize, cancellationToken);
        }
    }
}
