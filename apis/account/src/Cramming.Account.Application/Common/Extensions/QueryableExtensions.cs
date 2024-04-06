using Cramming.Account.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Cramming.Account.Application.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
            => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
    }
}
