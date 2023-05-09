namespace LibMpv.AutoGen.CppSharpUnsafeGenerator.Definitions;

internal record ExportFunctionDefinition : FunctionDefinitionBase
{
    public string LibraryName { get; init; }
    public int LibraryVersion { get; init; }
}
