namespace Cramming.SharedKernel
{
    public class BinaryContent(byte[] content, string type, string name)
    {
        public byte[] Content { get; init; } = content;

        public string Type { get; init; } = type;

        public string Name { get; init; } = name;

        public static BinaryContent Pdf(byte[] content, string name)
        {
            return new BinaryContent(content, "application/pdf", name);
        }
    };
}
