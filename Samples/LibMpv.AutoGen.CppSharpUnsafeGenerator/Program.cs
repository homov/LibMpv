using CppSharp.AST;
using LibMpv.AutoGen.CppSharpUnsafeGenerator.Generation;
using LibMpv.AutoGen.CppSharpUnsafeGenerator.Processing;

namespace LibMpv.AutoGen.CppSharpUnsafeGenerator;
internal class Program
{
    const string LibMpvIncludeDir = @"..\..\..\..\mpv\mpv-dev-x86_64\include\mpv";
    const string LibMpvBinDir = @"..\..\..\..\mpv\mpv-dev-x86_64";

    const string SolutionDir = @"..\..\..\..\";

    const string Namespace = "LibMpv.Client";
    const string TypeName = "libmpv";
    const bool SuppressUnmanagedCodeSecurity = true;

    private static void Main(string[] args)
    {
        var astContexts = Parse(LibMpvIncludeDir).ToList();
        var functionExports = FunctionExportHelper.LoadFunctionExports(LibMpvBinDir).ToArray();

        var processingContext = new ProcessingContext
        {
            //IgnoreUnitNames = new HashSet<string> { "__NSConstantString_tag" },
            TypeAliases = { 
                { "int64_t", typeof(long) }, 
                { "uint64_t", typeof(ulong) } 
            },
            WellKnownMacros =
            {
            },
            FunctionExportMap = functionExports
                .Where( x => x.Name.StartsWith("mpv_"))
                .GroupBy(x => x.Name)
                .Select(x => x.First()) // Eliminate duplicated names
                .ToDictionary(x => x.Name)
        };

        var processor = new ASTProcessor(processingContext);
        astContexts.ForEach(processor.Process);

        var generationContext = new GenerationContext
        {
            Namespace = Namespace,
            TypeName = TypeName,
            SuppressUnmanagedCodeSecurity = SuppressUnmanagedCodeSecurity,
            LibraryVersionMap = functionExports
                .Select(x => new { x.LibraryName, x.LibraryVersion })
                .Distinct()
                .ToDictionary(x => x.LibraryName, x => x.LibraryVersion),
            Definitions = processingContext.Definitions.ToArray(),
            SolutionDir = SolutionDir
        };

        GenerateLegacyLibMpvAutoGen(generationContext);
    }
    private static IEnumerable<ASTContext> Parse(string includesDir)
    {
        var p = new Parser
        {
            IncludeDirs = new[] { includesDir },
            Defines = new[] { "__STDC_CONSTANT_MACROS" }
        };

        yield return p.Parse("client.h");
        yield return p.Parse("render.h");
        yield return p.Parse("render_gl.h");
        yield return p.Parse("stream_cb.h");
    }

    private static void GenerateLegacyLibMpvAutoGen(GenerationContext baseContext)
    {
        var context = baseContext with
        {
            IsLegacyGenerationOn = true,
            OutputDir = Path.Combine(baseContext.SolutionDir, @"LibMpv.Client\generated")
        };

        LibrariesGenerator.Generate($"{context.TypeName}.libraries.g.cs", context);
        MacrosGenerator.Generate($"{context.TypeName}.macros.g.cs", context);
        EnumsGenerator.Generate("Enums.g.cs", context);
        DelegatesGenerator.Generate("Delegates.g.cs", context);
        FixedArraysGenerator.Generate("Arrays.g.cs", context);
        StructuresGenerator.Generate("Structs.g.cs", context);
        FunctionsGenerator.GenerateFacade($"{context.TypeName}.functions.facade.g.cs", context);
        FunctionsGenerator.GenerateVectors("vectors.g.cs", context with { TypeName = "vectors" });
        FunctionsGenerator.GenerateDynamicallyLoaded("DynamicallyLoadedBindings.g.cs", context with { TypeName = "DynamicallyLoadedBindings" });
    }
}