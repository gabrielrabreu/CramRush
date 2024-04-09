namespace Cramming.Application.Common.Interfaces
{
    public record FileComposed(byte[] Content, string ContentType, string Name);

    public interface IComposer<T>
    {
        FileComposed Compose(T topic);
    }
}
