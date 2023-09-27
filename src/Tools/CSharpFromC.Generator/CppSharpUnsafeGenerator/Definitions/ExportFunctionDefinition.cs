namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record ExportFunctionDefinition : FunctionDefinitionBase
{
    public string LibraryName { get; init; }
    public int LibraryVersion { get; init; }
    public string ExportName { get; set; }
}
