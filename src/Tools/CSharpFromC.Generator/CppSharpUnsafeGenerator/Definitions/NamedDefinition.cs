namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record NamedDefinition : ICanGenerateXmlDoc, IObsoletionAware
{
    public string Name { get; set; }
    public string TypeName { get; set; }
    public string Content { get; set; }
    public Obsoletion Obsoletion { get; set; }
}
