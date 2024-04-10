using Microsoft.EntityFrameworkCore;

namespace Cramming.Infrastructure.Data.Common
{
    public static class QueryableExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable,
                                                                                         int pageNumber,
                                                                                         int pageSize,
                                                                                         CancellationToken cancellationToken) where TDestination : class
            => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize, cancellationToken);
    }
}
