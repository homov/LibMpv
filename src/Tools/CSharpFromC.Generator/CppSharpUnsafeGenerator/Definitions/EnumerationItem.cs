namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record EnumerationItem : ICanGenerateXmlDoc
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string Content { get; set; }
}
