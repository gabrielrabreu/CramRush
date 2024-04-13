﻿using Cramming.SharedKernel;
using Cramming.UseCases.Topics;
using Cramming.UseCases.Topics.Search;

namespace Cramming.Infrastructure.Data.Queries
{
    public class SearchTopicQueryService(AppDbContext db) : ISearchTopicQueryService
    {
        public async Task<PagedList<TopicBriefDto>> SearchAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return await db.Topics
                .Select(topic => new TopicBriefDto(topic.Id, topic.Name))
                .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }
    }
}