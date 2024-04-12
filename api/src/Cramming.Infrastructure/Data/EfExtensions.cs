using Cramming.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Cramming.Infrastructure.Data
{
    public static class EfExtensions
    {
        public static async Task<PagedList<TDestination>> ToPagedListAsync<TDestination>(
            this IQueryable<TDestination> queryable,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default) where TDestination : class
        {
            var count = await queryable.CountAsync(cancellationToken);

            var items = await queryable.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<TDestination>(items, count, pageNumber, pageSize);
        }
    }
}
