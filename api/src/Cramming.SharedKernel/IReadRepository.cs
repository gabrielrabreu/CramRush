namespace Cramming.SharedKernel
{
    public interface IReadRepository<T> where T : IAggregateRoot
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
