using System;

namespace LibMpv.Client;
public partial class LibMpv
{

    internal static readonly int EAGAIN;

    internal static readonly int ENOMEM = 12;

    internal static readonly int EINVAL = 22;

    internal static readonly int EPIPE = 32;

    static LibMpv()
    {
        EAGAIN = FunctionResolverFactory.GetPlatformId() switch
        {
            LibMpvPlatformID.MacOSX => 35,
            _ => 11
        };

        DynamicallyLoadedBindings.Initialize();
    }

    /// <summary>
    ///     Gets or sets the root path for loading libraries.
    /// </summary>
    /// <value>The root path.</value>
    internal static string RootPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

    public static ConfigurationBuilder UseLibMpv(int version) =>new ConfigurationBuilder().UseLibMpv(version);
    

    public class ConfigurationBuilder
    {
        public ConfigurationBuilder UseLibMpv(int version)
        {
            LibraryVersionMap["libmpv"] = version;
            return this;
        }

        public ConfigurationBuilder InitAndroid(IntPtr jvmPtr)
        {
            LibMpv.MpvLavcSetJavaVm(jvmPtr, IntPtr.Zero);
            return this;
        }

        public ConfigurationBuilder UseLibraryPath(string path)
        {
            RootPath = path;
            return this;
        }
    }
}
