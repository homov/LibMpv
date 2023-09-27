using System;

namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record StructureDefinition : NamedDefinition, IDefinition
{
    public StructureField[] Fields { get; set; } = Array.Empty<StructureField>();
    public bool IsComplete { get; set; }
    public bool IsUnion { get; init; }
}
