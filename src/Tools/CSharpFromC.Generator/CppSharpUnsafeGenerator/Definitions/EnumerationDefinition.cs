using System;

namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record EnumerationDefinition : NamedDefinition, IDefinition
{
    public EnumerationItem[] Items { get; init; } = Array.Empty<EnumerationItem>();
}
