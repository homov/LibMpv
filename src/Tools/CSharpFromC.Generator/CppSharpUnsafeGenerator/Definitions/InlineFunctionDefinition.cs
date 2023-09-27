namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record InlineFunctionDefinition : FunctionDefinitionBase
{
    public string Body { get; init; }
    public string OriginalBodyHash { get; init; }
}
