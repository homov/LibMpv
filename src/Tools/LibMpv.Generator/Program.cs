namespace LibMpv.Generator;

internal class Program
{
    static void Main(string[] args)
    {
        string solutionPath = "../../../..";
        Generator.Generate(
            $"{solutionPath}/../../natives/windows/x86_64",
            $"{solutionPath}/../LibMpv.Client/Generated",
            "LibMpv.Client",
            "LibMpv"
        );
    }
}