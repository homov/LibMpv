namespace LibMpv.Client;

public static unsafe partial class LibMpv
{
    // public static MpvClientApiVersion = MPV_MAKE_VERSION(0x2, 0x1);
    /// <summary>MPV_ENABLE_DEPRECATED = 1</summary>
    public const int MpvEnableDeprecated = 0x1;
    // public static MpvExport = __declspec(dllexport);
    // public static MpvMakeVersion = major;
    // public static MpvOpenglDrmOsdSize = mpv_opengl_drm_draw_surface_size;
    /// <summary>MPV_RENDER_API_TYPE_OPENGL = &quot;opengl&quot;</summary>
    public const string MpvRenderApiTypeOpengl = "opengl";
    /// <summary>MPV_RENDER_API_TYPE_SW = &quot;sw&quot;</summary>
    public const string MpvRenderApiTypeSw = "sw";
    /// <summary>MPV_RENDER_PARAM_DRM_OSD_SIZE = 15</summary>
    public const int MpvRenderParamDrmOsdSize = 0xf;
}
