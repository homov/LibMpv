using System;

namespace LibMpv.AutoGen.CppSharpUnsafeGenerator.Definitions;

internal record EnumerationDefinition : NamedDefinition, IDefinition
{
    public EnumerationItem[] Items { get; init; } = Array.Empty<EnumerationItem>();
}
