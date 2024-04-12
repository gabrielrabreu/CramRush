﻿using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Search
{
    public interface ISearchTopicQueryService
    {
        Task<PagedList<TopicBriefDTO>> SearchAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
