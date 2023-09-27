namespace LibMpv.Client;

/// <summary>Since API version 1.9.</summary>
public enum MpvEndFileReason : int
{
    /// <summary>The end of file was reached. Sometimes this may also happen on incomplete or corrupted files, or if the network connection was interrupted when playing a remote file. It also happens if the playback range was restricted with --end or --frames or similar.</summary>
    MpvEndFileReasonEof = 0,
    /// <summary>Playback was stopped by an external action (e.g. playlist controls).</summary>
    MpvEndFileReasonStop = 2,
    /// <summary>Playback was stopped by the quit command or player shutdown.</summary>
    MpvEndFileReasonQuit = 3,
    /// <summary>Some kind of error happened that lead to playback abort. Does not necessarily happen on incomplete or broken files (in these cases, both MPV_END_FILE_REASON_ERROR or MPV_END_FILE_REASON_EOF are possible).</summary>
    MpvEndFileReasonError = 4,
    /// <summary>The file was a playlist or similar. When the playlist is read, its entries will be appended to the playlist after the entry of the current file, the entry of the current file is removed, and a MPV_EVENT_END_FILE event is sent with reason set to MPV_END_FILE_REASON_REDIRECT. Then playback continues with the playlist contents. Since API version 1.18.</summary>
    MpvEndFileReasonRedirect = 5,
}

/// <summary>List of error codes than can be returned by API functions. 0 and positive return values always mean success, negative values are always errors.</summary>
public enum MpvError : int
{
    /// <summary>No error happened (used to signal successful operation). Keep in mind that many API functions returning error codes can also return positive values, which also indicate success. API users can hardcode the fact that &quot;&gt;= 0&quot; means success.</summary>
    MpvErrorSuccess = 0,
    /// <summary>The event ringbuffer is full. This means the client is choked, and can&apos;t receive any events. This can happen when too many asynchronous requests have been made, but not answered. Probably never happens in practice, unless the mpv core is frozen for some reason, and the client keeps making asynchronous requests. (Bugs in the client API implementation could also trigger this, e.g. if events become &quot;lost&quot;.)</summary>
    MpvErrorEventQueueFull = -1,
    /// <summary>Memory allocation failed.</summary>
    MpvErrorNomem = -2,
    /// <summary>The mpv core wasn&apos;t configured and initialized yet. See the notes in mpv_create().</summary>
    MpvErrorUninitialized = -3,
    /// <summary>Generic catch-all error if a parameter is set to an invalid or unsupported value. This is used if there is no better error code.</summary>
    MpvErrorInvalidParameter = -4,
    /// <summary>Trying to set an option that doesn&apos;t exist.</summary>
    MpvErrorOptionNotFound = -5,
    /// <summary>Trying to set an option using an unsupported MPV_FORMAT.</summary>
    MpvErrorOptionFormat = -6,
    /// <summary>Setting the option failed. Typically this happens if the provided option value could not be parsed.</summary>
    MpvErrorOptionError = -7,
    /// <summary>The accessed property doesn&apos;t exist.</summary>
    MpvErrorPropertyNotFound = -8,
    /// <summary>Trying to set or get a property using an unsupported MPV_FORMAT.</summary>
    MpvErrorPropertyFormat = -9,
    /// <summary>The property exists, but is not available. This usually happens when the associated subsystem is not active, e.g. querying audio parameters while audio is disabled.</summary>
    MpvErrorPropertyUnavailable = -10,
    /// <summary>Error setting or getting a property.</summary>
    MpvErrorPropertyError = -11,
    /// <summary>General error when running a command with mpv_command and similar.</summary>
    MpvErrorCommand = -12,
    /// <summary>Generic error on loading (usually used with mpv_event_end_file.error).</summary>
    MpvErrorLoadingFailed = -13,
    /// <summary>Initializing the audio output failed.</summary>
    MpvErrorAoInitFailed = -14,
    /// <summary>Initializing the video output failed.</summary>
    MpvErrorVoInitFailed = -15,
    /// <summary>There was no audio or video data to play. This also happens if the file was recognized, but did not contain any audio or video streams, or no streams were selected.</summary>
    MpvErrorNothingToPlay = -16,
    /// <summary>When trying to load the file, the file format could not be determined, or the file was too broken to open it.</summary>
    MpvErrorUnknownFormat = -17,
    /// <summary>Generic error for signaling that certain system requirements are not fulfilled.</summary>
    MpvErrorUnsupported = -18,
    /// <summary>The API function which was called is a stub only.</summary>
    MpvErrorNotImplemented = -19,
    /// <summary>Unspecified error.</summary>
    MpvErrorGeneric = -20,
}

public enum MpvEventId : int
{
    /// <summary>Nothing happened. Happens on timeouts or sporadic wakeups.</summary>
    MpvEventNone = 0,
    /// <summary>Happens when the player quits. The player enters a state where it tries to disconnect all clients. Most requests to the player will fail, and the client should react to this and quit with mpv_destroy() as soon as possible.</summary>
    MpvEventShutdown = 1,
    /// <summary>See mpv_request_log_messages().</summary>
    MpvEventLogMessage = 2,
    /// <summary>Reply to a mpv_get_property_async() request. See also mpv_event and mpv_event_property.</summary>
    MpvEventGetPropertyReply = 3,
    /// <summary>Reply to a mpv_set_property_async() request. (Unlike MPV_EVENT_GET_PROPERTY, mpv_event_property is not used.)</summary>
    MpvEventSetPropertyReply = 4,
    /// <summary>Reply to a mpv_command_async() or mpv_command_node_async() request. See also mpv_event and mpv_event_command.</summary>
    MpvEventCommandReply = 5,
    /// <summary>Notification before playback start of a file (before the file is loaded). See also mpv_event and mpv_event_start_file.</summary>
    MpvEventStartFile = 6,
    /// <summary>Notification after playback end (after the file was unloaded). See also mpv_event and mpv_event_end_file.</summary>
    MpvEventEndFile = 7,
    /// <summary>Notification when the file has been loaded (headers were read etc.), and decoding starts.</summary>
    MpvEventFileLoaded = 8,
    /// <summary>Idle mode was entered. In this mode, no file is played, and the playback core waits for new commands. (The command line player normally quits instead of entering idle mode, unless --idle was specified. If mpv was started with mpv_create(), idle mode is enabled by default.)</summary>
    MpvEventIdle = 11,
    /// <summary>Sent every time after a video frame is displayed. Note that currently, this will be sent in lower frequency if there is no video, or playback is paused - but that will be removed in the future, and it will be restricted to video frames only.</summary>
    MpvEventTick = 14,
    /// <summary>Triggered by the script-message input command. The command uses the first argument of the command as client name (see mpv_client_name()) to dispatch the message, and passes along all arguments starting from the second argument as strings. See also mpv_event and mpv_event_client_message.</summary>
    MpvEventClientMessage = 16,
    /// <summary>Happens after video changed in some way. This can happen on resolution changes, pixel format changes, or video filter changes. The event is sent after the video filters and the VO are reconfigured. Applications embedding a mpv window should listen to this event in order to resize the window if needed. Note that this event can happen sporadically, and you should check yourself whether the video parameters really changed before doing something expensive.</summary>
    MpvEventVideoReconfig = 17,
    /// <summary>Similar to MPV_EVENT_VIDEO_RECONFIG. This is relatively uninteresting, because there is no such thing as audio output embedding.</summary>
    MpvEventAudioReconfig = 18,
    /// <summary>Happens when a seek was initiated. Playback stops. Usually it will resume with MPV_EVENT_PLAYBACK_RESTART as soon as the seek is finished.</summary>
    MpvEventSeek = 20,
    /// <summary>There was a discontinuity of some sort (like a seek), and playback was reinitialized. Usually happens on start of playback and after seeking. The main purpose is allowing the client to detect when a seek request is finished.</summary>
    MpvEventPlaybackRestart = 21,
    /// <summary>Event sent due to mpv_observe_property(). See also mpv_event and mpv_event_property.</summary>
    MpvEventPropertyChange = 22,
    /// <summary>Happens if the internal per-mpv_handle ringbuffer overflows, and at least 1 event had to be dropped. This can happen if the client doesn&apos;t read the event queue quickly enough with mpv_wait_event(), or if the client makes a very large number of asynchronous calls at once.</summary>
    MpvEventQueueOverflow = 24,
    /// <summary>Triggered if a hook handler was registered with mpv_hook_add(), and the hook is invoked. If you receive this, you must handle it, and continue the hook with mpv_hook_continue(). See also mpv_event and mpv_event_hook.</summary>
    MpvEventHook = 25,
}

/// <summary>Data format for options and properties. The API functions to get/set properties and options support multiple formats, and this enum describes them.</summary>
public enum MpvFormat : int
{
    /// <summary>Invalid. Sometimes used for empty values. This is always defined to 0, so a normal 0-init of mpv_format (or e.g. mpv_node) is guaranteed to set this it to MPV_FORMAT_NONE (which makes some things saner as consequence).</summary>
    MpvFormatNone = 0,
    /// <summary>The basic type is char*. It returns the raw property string, like using ${=property} in input.conf (see input.rst).</summary>
    MpvFormatString = 1,
    /// <summary>The basic type is char*. It returns the OSD property string, like using ${property} in input.conf (see input.rst). In many cases, this is the same as the raw string, but in other cases it&apos;s formatted for display on OSD. It&apos;s intended to be human readable. Do not attempt to parse these strings.</summary>
    MpvFormatOsdString = 2,
    /// <summary>The basic type is int. The only allowed values are 0 (&quot;no&quot;) and 1 (&quot;yes&quot;).</summary>
    MpvFormatFlag = 3,
    /// <summary>The basic type is int64_t.</summary>
    MpvFormatInt64 = 4,
    /// <summary>The basic type is double.</summary>
    MpvFormatDouble = 5,
    /// <summary>The type is mpv_node.</summary>
    MpvFormatNode = 6,
    /// <summary>Used with mpv_node only. Can usually not be used directly.</summary>
    MpvFormatNodeArray = 7,
    /// <summary>See MPV_FORMAT_NODE_ARRAY.</summary>
    MpvFormatNodeMap = 8,
    /// <summary>A raw, untyped byte array. Only used only with mpv_node, and only in some very specific situations. (Some commands use it.)</summary>
    MpvFormatByteArray = 9,
}

/// <summary>Numeric log levels. The lower the number, the more important the message is. MPV_LOG_LEVEL_NONE is never used when receiving messages. The string in the comment after the value is the name of the log level as used for the mpv_request_log_messages() function. Unused numeric values are unused, but reserved for future use.</summary>
public enum MpvLogLevel : int
{
    MpvLogLevelNone = 0,
    /// <summary>&quot;no&quot; - disable absolutely all messages</summary>
    MpvLogLevelFatal = 10,
    /// <summary>&quot;fatal&quot; - critical/aborting errors</summary>
    MpvLogLevelError = 20,
    /// <summary>&quot;error&quot; - simple errors</summary>
    MpvLogLevelWarn = 30,
    /// <summary>&quot;warn&quot; - possible problems</summary>
    MpvLogLevelInfo = 40,
    /// <summary>&quot;info&quot; - informational message</summary>
    MpvLogLevelV = 50,
    /// <summary>&quot;v&quot; - noisy informational message</summary>
    MpvLogLevelDebug = 60,
    /// <summary>&quot;debug&quot; - very noisy technical information</summary>
    MpvLogLevelTrace = 70,
}

/// <summary>Flags used in mpv_render_frame_info.flags. Each value represents a bit in it.</summary>
public enum MpvRenderFrameInfoFlag : int
{
    /// <summary>Set if there is actually a next frame. If unset, there is no next frame yet, and other flags and fields that require a frame to be queued will be unset.</summary>
    MpvRenderFrameInfoPresent = 1,
    /// <summary>If set, the frame is not an actual new video frame, but a redraw request. For example if the video is paused, and an option that affects video rendering was changed (or any other reason), an update request can be issued and this flag will be set.</summary>
    MpvRenderFrameInfoRedraw = 2,
    /// <summary>If set, this is supposed to reproduce the previous frame perfectly. This is usually used for certain &quot;video-sync&quot; options (&quot;display-...&quot; modes). Typically the renderer will blit the video from a FBO. Unset otherwise.</summary>
    MpvRenderFrameInfoRepeat = 4,
    /// <summary>If set, the player timing code expects that the user thread blocks on vsync (by either delaying the render call, or by making a call to mpv_render_context_report_swap() at vsync time).</summary>
    MpvRenderFrameInfoBlockVsync = 8,
}

/// <summary>Parameters for mpv_render_param (which is used in a few places such as mpv_render_context_create().</summary>
public enum MpvRenderParamType : int
{
    /// <summary>Not a valid value, but also used to terminate a params array. Its value is always guaranteed to be 0 (even if the ABI changes in the future).</summary>
    MpvRenderParamInvalid = 0,
    /// <summary>The render API to use. Valid for mpv_render_context_create().</summary>
    MpvRenderParamApiType = 1,
    /// <summary>Required parameters for initializing the OpenGL renderer. Valid for mpv_render_context_create(). Type: mpv_opengl_init_params*</summary>
    MpvRenderParamOpenglInitParams = 2,
    /// <summary>Describes a GL render target. Valid for mpv_render_context_render(). Type: mpv_opengl_fbo*</summary>
    MpvRenderParamOpenglFbo = 3,
    /// <summary>Control flipped rendering. Valid for mpv_render_context_render(). Type: int* If the value is set to 0, render normally. Otherwise, render it flipped, which is needed e.g. when rendering to an OpenGL default framebuffer (which has a flipped coordinate system).</summary>
    MpvRenderParamFlipY = 4,
    /// <summary>Control surface depth. Valid for mpv_render_context_render(). Type: int* This implies the depth of the surface passed to the render function in bits per channel. If omitted or set to 0, the renderer will assume 8. Typically used to control dithering.</summary>
    MpvRenderParamDepth = 5,
    /// <summary>ICC profile blob. Valid for mpv_render_context_set_parameter(). Type: mpv_byte_array* Set an ICC profile for use with the &quot;icc-profile-auto&quot; option. (If the option is not enabled, the ICC data will not be used.)</summary>
    MpvRenderParamIccProfile = 6,
    /// <summary>Ambient light in lux. Valid for mpv_render_context_set_parameter(). Type: int* This can be used for automatic gamma correction.</summary>
    MpvRenderParamAmbientLight = 7,
    /// <summary>X11 Display, sometimes used for hwdec. Valid for mpv_render_context_create(). The Display must stay valid for the lifetime of the mpv_render_context. Type: Display*</summary>
    MpvRenderParamX11Display = 8,
    /// <summary>Wayland display, sometimes used for hwdec. Valid for mpv_render_context_create(). The wl_display must stay valid for the lifetime of the mpv_render_context. Type: struct wl_display*</summary>
    MpvRenderParamWlDisplay = 9,
    /// <summary>Better control about rendering and enabling some advanced features. Valid for mpv_render_context_create().</summary>
    MpvRenderParamAdvancedControl = 10,
    /// <summary>Return information about the next frame to render. Valid for mpv_render_context_get_info().</summary>
    MpvRenderParamNextFrameInfo = 11,
    /// <summary>Enable or disable video timing. Valid for mpv_render_context_render().</summary>
    MpvRenderParamBlockForTargetTime = 12,
    /// <summary>Use to skip rendering in mpv_render_context_render().</summary>
    MpvRenderParamSkipRendering = 13,
    /// <summary>Deprecated. Not supported. Use MPV_RENDER_PARAM_DRM_DISPLAY_V2 instead. Type : struct mpv_opengl_drm_params*</summary>
    MpvRenderParamDrmDisplay = 14,
    /// <summary>DRM draw surface size, contains draw surface dimensions. Valid for mpv_render_context_create(). Type : struct mpv_opengl_drm_draw_surface_size*</summary>
    MpvRenderParamDrmDrawSurfaceSize = 15,
    /// <summary>DRM display, contains drm display handles. Valid for mpv_render_context_create(). Type : struct mpv_opengl_drm_params_v2*</summary>
    MpvRenderParamDrmDisplayV2 = 16,
    /// <summary>MPV_RENDER_API_TYPE_SW only: rendering target surface size, mandatory. Valid for MPV_RENDER_API_TYPE_SW &amp; mpv_render_context_render(). Type: int[2] (e.g.: int s[2] = {w, h}; param.data = &amp;s[0];)</summary>
    MpvRenderParamSwSize = 17,
    /// <summary>MPV_RENDER_API_TYPE_SW only: rendering target surface pixel format, mandatory. Valid for MPV_RENDER_API_TYPE_SW &amp; mpv_render_context_render(). Type: char* (e.g.: char *f = &quot;rgb0&quot;; param.data = f;)</summary>
    MpvRenderParamSwFormat = 18,
    /// <summary>MPV_RENDER_API_TYPE_SW only: rendering target surface bytes per line, mandatory. Valid for MPV_RENDER_API_TYPE_SW &amp; mpv_render_context_render(). Type: size_t*</summary>
    MpvRenderParamSwStride = 19,
    /// <summary>MPV_RENDER_API_TYPE_SW only: rendering target surface bytes per line, mandatory. Valid for MPV_RENDER_API_TYPE_SW &amp; mpv_render_context_render(). Type: size_t*</summary>
    MpvRenderParamSwPointer = 20,
}

/// <summary>Flags returned by mpv_render_context_update(). Each value represents a bit in the function&apos;s return value.</summary>
public enum MpvRenderUpdateFlag : int
{
    /// <summary>A new video frame must be rendered. mpv_render_context_render() must be called.</summary>
    MpvRenderUpdateFrame = 1,
}

