using System;

namespace LibMpv.AutoGen.CppSharpUnsafeGenerator.Definitions;

internal record StructureDefinition : NamedDefinition, IDefinition
{
    public StructureField[] Fields { get; set; } = Array.Empty<StructureField>();
    public bool IsComplete { get; set; }
    public bool IsUnion { get; init; }
}
