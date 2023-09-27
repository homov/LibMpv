namespace CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;

public record FixedArrayDefinition : TypeDefinition
{
    public TypeDefinition ElementType { get; init; }
    public int Length { get; init; }
    public bool IsPrimitive { get; init; }
    public bool IsPointer { get; init; }
}
