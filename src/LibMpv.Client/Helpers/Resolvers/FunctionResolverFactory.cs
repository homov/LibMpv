using System.Runtime.InteropServices;

namespace LibMpv.Client;

public enum LibMpvPlatformID 
{
    Win32NT = 1,
    Unix = 2,
    MacOSX = 3,
    Android = 4,
    Other = 10
}

public static class FunctionResolverFactory
{
    public static LibMpvPlatformID GetPlatformId()
    {
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Win32NT:
                return LibMpvPlatformID.Win32NT;
            case PlatformID.Unix:
                {
                    var isAndroid = RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID"));
                    return isAndroid  ? LibMpvPlatformID.Android : LibMpvPlatformID.Unix;
                }
            case PlatformID.MacOSX:
                return LibMpvPlatformID.MacOSX;
            default:
                return LibMpvPlatformID.Other;
        }
    }

    public static IFunctionResolver Create()
    {
        var os = System.Environment.OSVersion;
        switch (GetPlatformId())
        {
            case LibMpvPlatformID.MacOSX:
                return new MacFunctionResolver();
            case LibMpvPlatformID.Unix:
                return new LinuxFunctionResolver();
            case LibMpvPlatformID.Android:
                return new AndroidFunctionResolver();
            case LibMpvPlatformID.Win32NT:
                return new WindowsFunctionResolver();
            default:
                throw new PlatformNotSupportedException();
        }
    }
}
