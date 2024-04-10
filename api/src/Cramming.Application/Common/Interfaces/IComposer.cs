namespace Cramming.Application.Common.Interfaces
{
    public record FileComposed(byte[] Content, string ContentType, string Name);

    public interface IComposer<in T>
    {
        FileComposed Compose(T topic);
    }
}
