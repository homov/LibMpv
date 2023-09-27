using CppSharp.AST;
using CSharpFromC.Generator.CppSharpUnsafeGenerator;
using CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions;
using CSharpFromC.Generator.CppSharpUnsafeGenerator.Generation;
using CSharpFromC.Generator.CppSharpUnsafeGenerator.Processing;
using MoreLinq;
using MacroDefinition = CSharpFromC.Generator.CppSharpUnsafeGenerator.Definitions.MacroDefinition;

namespace LibMpv.Generator;

public static class Generator
{

    public static void Generate(string libmpPath, string outputPath, string nameSpace, string typeName)
    {
        var astContexts = Parse(libmpPath + "/include/mpv").ToList();

        var functionExports = FunctionExportHelper.LoadFunctionExports(libmpPath).ToList();

        functionExports.ForEach(it => it.LibraryVersion = 0);

        functionExports.Add(new FunctionExport()
        {
            LibraryName = "libmpv",
            LibraryVersion = 0,
            Name = "mpv_lavc_set_java_vm"
        });

        var processingContext = new ProcessingContext
        {
            //IgnoreUnitNames = new HashSet<string> { "__NSConstantString_tag" },
            TypeAliases = {
                { "int64_t", typeof(long) },
                { "uint64_t", typeof(ulong) }
            },
            WellKnownMacros ={},
            FunctionExportMap = functionExports
                .Where(x => x.Name.StartsWith("mpv_"))
                .GroupBy(x => x.Name)
                .Select(x => x.First()) // Eliminate duplicated names
                .ToDictionary(x => x.Name)
        };

        var processor = new ASTProcessor(processingContext);
        astContexts.ForEach(processor.Process);

        var generationContext = new GenerationContext
        {
            Namespace = nameSpace,
            TypeName = typeName,
            SuppressUnmanagedCodeSecurity = true,
            LibraryVersionMap = functionExports
                .Select(x => new { x.LibraryName, x.LibraryVersion })
                .Distinct()
                .ToDictionary(x => x.LibraryName, x => x.LibraryVersion),

            Definitions = processingContext.Definitions.ToList(),
            SolutionDir = outputPath
        };

        generationContext.Definitions.Add(new ExportFunctionDefinition()
        {
            LibraryName = "libmpv",
            LibraryVersion = 0,
            ExportName = "mpv_lavc_set_java_vm",
            Name = "mpv_lavc_set_java_vm",
            ReturnType = new TypeDefinition() { Name = "int", ByReference = false },
            Parameters = new FunctionParameter[] { new FunctionParameter()
            {
                ByReference = false,
                Name = "jvm",
                Type = new TypeDefinition() {  Name = "IntPtr", ByReference = false },
                Content = ""

            }},
            Content = "Initialize JVM on android",
            ReturnComment = "error code"

        });
        Rename(generationContext);
        Generate(generationContext);
    }

    private static void Rename(GenerationContext baseContext)
    {
        foreach (var definition in baseContext.Definitions)
        {
            Rename(definition);
        }
    }

    private static string CamelCase(string name)
    {
        if (String.IsNullOrEmpty(name))
            return name;
        else if (name.StartsWith("_drmModeAtomicReq"))
            return name.Replace("_drmModeAtomicReq", "DrmModeAtomicReq");

        return string.Join("", name.Split('_').Where(it => it.Length > 0).Select(it => char.ToUpperInvariant(it[0]) + it.Substring(1).ToLowerInvariant()));
    }

    private static string RenameStructMemberName(string name)
    {
        if (name == "string")
            return "StringField";
        else if (name == "int64")
            return "LongField";
        else if (name == "double_")
            return "DoubleField";
        else if (name.StartsWith("_drmModeAtomicReq"))
            return name.Replace("_drmModeAtomicReq", "DrmModeAtomicReq");

        return CamelCase(name);
    }

    private static string RenameTypeName(string name)
    {
        if (name.StartsWith("mpv_"))
            return CamelCase(name);
        else if (name.StartsWith("_drmModeAtomicReq"))
            return name.Replace("_drmModeAtomicReq", "DrmModeAtomicReq");
        return name;
    }

    private static void Rename(IDefinition definition)
    {
        if (definition is MacroDefinition macroDef)
        {
            macroDef.Name = CamelCase(macroDef.Name);
        }
        else if (definition is EnumerationDefinition enumDef)
        {
            enumDef.Name = CamelCase(enumDef.Name);
            enumDef.Items.ForEach(item => item.Name = CamelCase(item.Name));
        }
        else if (definition is DelegateDefinition delegDef)
        {
            delegDef.Name = CamelCase(delegDef.Name);
            delegDef.FunctionName = CamelCase(delegDef.FunctionName);
            delegDef.ReturnType.Name = RenameTypeName(delegDef.ReturnType.Name);
            delegDef.Parameters.ForEach(it => it.Type.Name = RenameTypeName(it.Type.Name));
        }
        else if (definition is StructureDefinition structDef)
        {
            structDef.Name = CamelCase(structDef.Name);
            structDef.Fields.ForEach(it =>
            {
                it.Name = RenameStructMemberName(it.Name);
                it.FieldType.Name = RenameTypeName(it.FieldType.Name);
            });
        }
        else if (definition is ExportFunctionDefinition funcDef)
        {
            funcDef.Name = CamelCase(funcDef.Name);
            funcDef.ReturnType.Name = RenameTypeName(funcDef.ReturnType.Name);
            funcDef.Parameters.ForEach(it =>
            {
                it.Type.Name = RenameTypeName(it.Type.Name);
            });
        }
    }

    private static void Generate(GenerationContext baseContext)
    {
        var context = baseContext with
        {
            IsLegacyGenerationOn = true,
            OutputDir = System.IO.Path.GetFullPath(baseContext.SolutionDir)
        };


        LibrariesGenerator.Generate($"{context.TypeName}.libraries.generated.cs", context);
        MacrosGenerator.Generate($"{context.TypeName}.macros.generated.cs", context);
        EnumsGenerator.Generate("Enums.generated.cs", context);
        DelegatesGenerator.Generate("Delegates.generated.cs", context);
        StructuresGenerator.Generate("Structs.generated.cs", context);
        FunctionsGenerator.GenerateFacade($"{context.TypeName}.functions.facade.generated.cs", context);
        FunctionsGenerator.GenerateVectors("Vectors.generated.cs", context with { TypeName = "Vectors" });
        FunctionsGenerator.GenerateDynamicallyLoaded("DynamicallyLoadedBindings.generated.cs", context with { TypeName = "DynamicallyLoadedBindings" });
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
}
