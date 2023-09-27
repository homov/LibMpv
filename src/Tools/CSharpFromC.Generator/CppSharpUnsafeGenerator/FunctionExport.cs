using System.Diagnostics;

namespace CSharpFromC.Generator.CppSharpUnsafeGenerator;

[DebuggerDisplay("{Name}, {LibraryName}-{LibraryVersion}")]
public record FunctionExport
{
    public string Name { get; set; }
    public string LibraryName { get; init; }
    public int LibraryVersion { get; set; }
    public string ExportName { get; internal set; }
}
