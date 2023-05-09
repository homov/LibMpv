namespace LibMpv.Client;

public static unsafe partial class libmpv
{
    // public static MPV_CLIENT_API_VERSION = MPV_MAKE_VERSION(0x2, 0x1);
    /// <summary>MPV_ENABLE_DEPRECATED = 1</summary>
    public const int MPV_ENABLE_DEPRECATED = 0x1;
    // public static MPV_EXPORT = __declspec(dllexport);
    // public static MPV_MAKE_VERSION = major;
    // public static mpv_opengl_drm_osd_size = mpv_opengl_drm_draw_surface_size;
    /// <summary>MPV_RENDER_API_TYPE_OPENGL = &quot;opengl&quot;</summary>
    public const string MPV_RENDER_API_TYPE_OPENGL = "opengl";
    /// <summary>MPV_RENDER_API_TYPE_SW = &quot;sw&quot;</summary>
    public const string MPV_RENDER_API_TYPE_SW = "sw";
    /// <summary>MPV_RENDER_PARAM_DRM_OSD_SIZE = 15</summary>
    public const int MPV_RENDER_PARAM_DRM_OSD_SIZE = 0xf;
}
