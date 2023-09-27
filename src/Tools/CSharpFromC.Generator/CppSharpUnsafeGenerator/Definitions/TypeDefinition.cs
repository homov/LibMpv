using System;

namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record TypeDefinition : IDefinition
{
    public string[] Attributes { get; init; } = Array.Empty<string>();
    public bool ByReference { get; init; }
    public string Name { get; set; }
    public string LegacyName { get; init; }
}
