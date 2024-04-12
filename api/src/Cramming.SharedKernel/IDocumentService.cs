namespace Cramming.SharedKernel
{
    public interface IDocumentService<T> where T : class
    {
        Task<Document> ComposeAsync(T model, CancellationToken cancellationToken = default);
    }
}
