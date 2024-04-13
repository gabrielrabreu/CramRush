namespace Cramming.SharedKernel
{
    public interface IPdfService<in T> where T : class
    {
        BinaryContent Create(T model);
    }
}
