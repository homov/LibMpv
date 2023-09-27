using System.Collections.Generic;

namespace LibMpv.Client;

public static unsafe partial class LibMpv
{
    public static Dictionary<string, int> LibraryVersionMap = new Dictionary<string, int>
    {
        {"libmpv", 0},
    };
}
