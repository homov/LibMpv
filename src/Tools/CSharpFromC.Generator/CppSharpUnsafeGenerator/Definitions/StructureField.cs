namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record StructureField : ICanGenerateXmlDoc, IObsoletionAware
{
    public string Name { get; set; }
    public TypeDefinition FieldType { get; init; }
    public string Content { get; init; }
    public Obsoletion Obsoletion { get; init; }
}
