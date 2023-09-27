using System;

namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record DelegateDefinition : TypeDefinition
{
    public string FunctionName { get; set; }
    public TypeDefinition ReturnType { get; init; }
    public FunctionParameter[] Parameters { get; init; } = Array.Empty<FunctionParameter>();
}
