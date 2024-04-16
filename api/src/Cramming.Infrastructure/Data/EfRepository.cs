namespace Cramming.Infrastructure.Data
{
    public class EfRepository<T>(
        AppDbContext db)
        : IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly AppDbContext Db = db;

        public virtual async Task<List<T>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await Db.Set<T>()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await Db.Set<T>()
                .FindAsync([id], cancellationToken);
        }

        public virtual async Task<T> AddAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await Db.Set<T>()
                .AddAsync(entity, cancellationToken);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual async Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            Db.Set<T>()
                .Update(entity);

            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            Db.Set<T>()
                .Remove(entity);

            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await Db.SaveChangesAsync(cancellationToken);
        }
    }
}
