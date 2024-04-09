using Cramming.Application.Common.Interfaces;
using Cramming.Domain.Entities;
using Cramming.Infrastructure.Data.Context;
using Cramming.Infrastructure.Data.Entities;

namespace Cramming.Infrastructure.Data.Repositories
{
    public class TopicRepository(ApplicationDbContext context) : ITopicRepository
    {
        public async Task<TopicEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var data = await context.Topics.FindAsync([id], cancellationToken);
            
            if (data == null) return null;

            await context.Entry(data).Collection(p => p.Tags).LoadAsync(cancellationToken);
            
            await context.Entry(data).Collection(p => p.Questions).LoadAsync(cancellationToken);

            var tags = new List<TopicTagEntity>();
            foreach (var tag in data.Tags)
            {
                tags.Add(new TopicTagEntity(tag.Id, tag.TopicId, tag.Name));
            }

            var questions = new List<TopicQuestionEntity>();
            foreach (var question in data.Questions)
            {
                if (question is TopicOpenEndedQuestionData openEndedQuestion)
                    questions.Add(new TopicOpenEndedQuestionEntity(openEndedQuestion.Id,
                                                                   openEndedQuestion.TopicId,
                                                                   openEndedQuestion.Statement,
                                                                   openEndedQuestion.Answer));

                else if (question is TopicMultipleChoiceQuestionData multipleChoiceQuestion)
                    questions.Add(new TopicMultipleChoiceQuestionEntity(multipleChoiceQuestion.Id,
                                                                        multipleChoiceQuestion.TopicId,
                                                                        multipleChoiceQuestion.Statement,
                                                                        multipleChoiceQuestion.Options.Select(option => 
                                                                        new TopicMultipleChoiceQuestionOptionEntity(
                                                                                option.Id,
                                                                                option.QuestionId,
                                                                                option.Statement,
                                                                                option.IsAnswer)).ToList()));
            }

            return new TopicEntity(data.Id, data.Name, data.Description, tags, questions);
        }

        public async Task<TopicEntity> AddAsync(TopicEntity domain, CancellationToken cancellationToken)
        {
            var data = new TopicData
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Tags = [],
                Questions = []
            };

            await context.Topics.AddAsync(data, cancellationToken);

            return domain;
        }

        public async Task UpdateAsync(TopicEntity domain, CancellationToken cancellationToken)
        {
            var data = await context.Topics.FindAsync([domain.Id], cancellationToken);

            if (data != null)
            {
                await context.Entry(data).Collection(p => p.Tags).LoadAsync(cancellationToken);

                await context.Entry(data).Collection(p => p.Questions).LoadAsync(cancellationToken);

                foreach (var existingTag in data.Tags)
                {
                    if (!domain.Tags.Any(p => p.Id == existingTag.Id))
                        context.TopicTags.Remove(existingTag);
                }

                foreach (var tag in domain.Tags)
                {
                    var existingTag = data.Tags.SingleOrDefault(p => p.Id == tag.Id);

                    if (existingTag == null)
                        await context.TopicTags.AddAsync(
                            new TopicTagData
                            {
                                Id = tag.Id,
                                TopicId = tag.TopicId,
                                Name = tag.Name
                            }, 
                            cancellationToken);
                }

                foreach (var existingQuestion in data.Questions)
                {
                    if (!domain.Questions.Any(p => p.Id == existingQuestion.Id))
                        context.TopicQuestions.Remove(existingQuestion);
                }

                foreach (var question in domain.Questions)
                {
                    var existingQuestion = data.Questions.SingleOrDefault(p => p.Id == question.Id);

                    if (existingQuestion == null)
                    {
                        if (question is TopicOpenEndedQuestionEntity openEndedQuestion)
                        {
                            await context.TopicQuestions.AddAsync(
                                new TopicOpenEndedQuestionData
                                {
                                    Id = openEndedQuestion.Id,
                                    TopicId = openEndedQuestion.TopicId,
                                    Statement = openEndedQuestion.Statement,
                                    Answer = openEndedQuestion.Answer
                                },
                                cancellationToken);
                        }
                        else if (question is TopicMultipleChoiceQuestionEntity multipleChoiceQuestion)
                        {
                            await context.TopicQuestions.AddAsync(
                                new TopicMultipleChoiceQuestionData
                                {
                                    Id = multipleChoiceQuestion.Id,
                                    TopicId = multipleChoiceQuestion.TopicId,
                                    Statement = multipleChoiceQuestion.Statement,
                                    Options = multipleChoiceQuestion.Options.Select(option =>
                                        new TopicMultipleChoiceQuestionOptionData
                                        {
                                            Id = option.Id,
                                            QuestionId = option.QuestionId,
                                            Statement = option.Statement,
                                            IsAnswer = option.IsAnswer
                                        }).ToList()
                                },
                                cancellationToken);
                        }
                    }
                }
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
