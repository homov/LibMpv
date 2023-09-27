using System;
using System.Collections.Generic;
using CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Generation;

public sealed record GenerationContext
{
    public string Namespace { get; init; } = string.Empty;
    public string TypeName { get; init; } = string.Empty;
    public bool SuppressUnmanagedCodeSecurity { get; init; }
    public bool IsLegacyGenerationOn { get; init; }
    public Dictionary<string, int> LibraryVersionMap { get; init; } = new();
    public List<IDefinition> Definitions { get; init; } = new();
    public Dictionary<string, InlineFunctionDefinition> ExistingInlineFunctionMap { get; init; } = new();
    public string SolutionDir { get; init; } = string.Empty;
    public string OutputDir { get; init; } = string.Empty;
}
