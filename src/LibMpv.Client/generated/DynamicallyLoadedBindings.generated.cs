using System;
using System.Security;
using System.Runtime.InteropServices;

namespace LibMpv.Client;

public static unsafe partial class DynamicallyLoadedBindings
{
    public static bool ThrowErrorIfFunctionNotFound;
    public static IFunctionResolver FunctionResolver;
    
    public unsafe static void Initialize()
    {
        if (FunctionResolver == null) FunctionResolver = FunctionResolverFactory.Create();
        
        Vectors.MpvAbortAsyncCommand = (MpvHandle* @ctx, ulong @reply_userdata) =>
        {
            Vectors.MpvAbortAsyncCommand = FunctionResolver.GetFunctionDelegate<Vectors.MpvAbortAsyncCommandDelegate>("libmpv", "mpv_abort_async_command", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvAbortAsyncCommand(@ctx, @reply_userdata);
        };
        
        Vectors.MpvClientApiVersion = () =>
        {
            Vectors.MpvClientApiVersion = FunctionResolver.GetFunctionDelegate<Vectors.MpvClientApiVersionDelegate>("libmpv", "mpv_client_api_version", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvClientApiVersion();
        };
        
        Vectors.MpvClientId = (MpvHandle* @ctx) =>
        {
            Vectors.MpvClientId = FunctionResolver.GetFunctionDelegate<Vectors.MpvClientIdDelegate>("libmpv", "mpv_client_id", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvClientId(@ctx);
        };
        
        Vectors.MpvClientName = (MpvHandle* @ctx) =>
        {
            Vectors.MpvClientName = FunctionResolver.GetFunctionDelegate<Vectors.MpvClientNameDelegate>("libmpv", "mpv_client_name", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvClientName(@ctx);
        };
        
        Vectors.MpvCommand = (MpvHandle* @ctx, byte** @args) =>
        {
            Vectors.MpvCommand = FunctionResolver.GetFunctionDelegate<Vectors.MpvCommandDelegate>("libmpv", "mpv_command", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCommand(@ctx, @args);
        };
        
        Vectors.MpvCommandAsync = (MpvHandle* @ctx, ulong @reply_userdata, byte** @args) =>
        {
            Vectors.MpvCommandAsync = FunctionResolver.GetFunctionDelegate<Vectors.MpvCommandAsyncDelegate>("libmpv", "mpv_command_async", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCommandAsync(@ctx, @reply_userdata, @args);
        };
        
        Vectors.MpvCommandNode = (MpvHandle* @ctx, MpvNode* @args, MpvNode* @result) =>
        {
            Vectors.MpvCommandNode = FunctionResolver.GetFunctionDelegate<Vectors.MpvCommandNodeDelegate>("libmpv", "mpv_command_node", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCommandNode(@ctx, @args, @result);
        };
        
        Vectors.MpvCommandNodeAsync = (MpvHandle* @ctx, ulong @reply_userdata, MpvNode* @args) =>
        {
            Vectors.MpvCommandNodeAsync = FunctionResolver.GetFunctionDelegate<Vectors.MpvCommandNodeAsyncDelegate>("libmpv", "mpv_command_node_async", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCommandNodeAsync(@ctx, @reply_userdata, @args);
        };
        
        Vectors.MpvCommandRet = (MpvHandle* @ctx, byte** @args, MpvNode* @result) =>
        {
            Vectors.MpvCommandRet = FunctionResolver.GetFunctionDelegate<Vectors.MpvCommandRetDelegate>("libmpv", "mpv_command_ret", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCommandRet(@ctx, @args, @result);
        };
        
        Vectors.MpvCommandString = (MpvHandle* @ctx, string @args) =>
        {
            Vectors.MpvCommandString = FunctionResolver.GetFunctionDelegate<Vectors.MpvCommandStringDelegate>("libmpv", "mpv_command_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCommandString(@ctx, @args);
        };
        
        Vectors.MpvCreate = () =>
        {
            Vectors.MpvCreate = FunctionResolver.GetFunctionDelegate<Vectors.MpvCreateDelegate>("libmpv", "mpv_create", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCreate();
        };
        
        Vectors.MpvCreateClient = (MpvHandle* @ctx, string @name) =>
        {
            Vectors.MpvCreateClient = FunctionResolver.GetFunctionDelegate<Vectors.MpvCreateClientDelegate>("libmpv", "mpv_create_client", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCreateClient(@ctx, @name);
        };
        
        Vectors.MpvCreateWeakClient = (MpvHandle* @ctx, string @name) =>
        {
            Vectors.MpvCreateWeakClient = FunctionResolver.GetFunctionDelegate<Vectors.MpvCreateWeakClientDelegate>("libmpv", "mpv_create_weak_client", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvCreateWeakClient(@ctx, @name);
        };
        
        Vectors.MpvDelProperty = (MpvHandle* @ctx, string @name) =>
        {
            Vectors.MpvDelProperty = FunctionResolver.GetFunctionDelegate<Vectors.MpvDelPropertyDelegate>("libmpv", "mpv_del_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvDelProperty(@ctx, @name);
        };
        
        Vectors.MpvDestroy = (MpvHandle* @ctx) =>
        {
            Vectors.MpvDestroy = FunctionResolver.GetFunctionDelegate<Vectors.MpvDestroyDelegate>("libmpv", "mpv_destroy", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvDestroy(@ctx);
        };
        
        Vectors.MpvErrorString = (int @error) =>
        {
            Vectors.MpvErrorString = FunctionResolver.GetFunctionDelegate<Vectors.MpvErrorStringDelegate>("libmpv", "mpv_error_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvErrorString(@error);
        };
        
        Vectors.MpvEventName = (MpvEventId @event) =>
        {
            Vectors.MpvEventName = FunctionResolver.GetFunctionDelegate<Vectors.MpvEventNameDelegate>("libmpv", "mpv_event_name", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvEventName(@event);
        };
        
        Vectors.MpvEventToNode = (MpvNode* @dst, MpvEvent* @src) =>
        {
            Vectors.MpvEventToNode = FunctionResolver.GetFunctionDelegate<Vectors.MpvEventToNodeDelegate>("libmpv", "mpv_event_to_node", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvEventToNode(@dst, @src);
        };
        
        Vectors.MpvFree = (void* @data) =>
        {
            Vectors.MpvFree = FunctionResolver.GetFunctionDelegate<Vectors.MpvFreeDelegate>("libmpv", "mpv_free", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvFree(@data);
        };
        
        Vectors.MpvFreeNodeContents = (MpvNode* @node) =>
        {
            Vectors.MpvFreeNodeContents = FunctionResolver.GetFunctionDelegate<Vectors.MpvFreeNodeContentsDelegate>("libmpv", "mpv_free_node_contents", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvFreeNodeContents(@node);
        };
        
        Vectors.MpvGetProperty = (MpvHandle* @ctx, string @name, MpvFormat @format, void* @data) =>
        {
            Vectors.MpvGetProperty = FunctionResolver.GetFunctionDelegate<Vectors.MpvGetPropertyDelegate>("libmpv", "mpv_get_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvGetProperty(@ctx, @name, @format, @data);
        };
        
        Vectors.MpvGetPropertyAsync = (MpvHandle* @ctx, ulong @reply_userdata, string @name, MpvFormat @format) =>
        {
            Vectors.MpvGetPropertyAsync = FunctionResolver.GetFunctionDelegate<Vectors.MpvGetPropertyAsyncDelegate>("libmpv", "mpv_get_property_async", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvGetPropertyAsync(@ctx, @reply_userdata, @name, @format);
        };
        
        Vectors.MpvGetPropertyOsdString = (MpvHandle* @ctx, string @name) =>
        {
            Vectors.MpvGetPropertyOsdString = FunctionResolver.GetFunctionDelegate<Vectors.MpvGetPropertyOsdStringDelegate>("libmpv", "mpv_get_property_osd_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvGetPropertyOsdString(@ctx, @name);
        };
        
        Vectors.MpvGetPropertyString = (MpvHandle* @ctx, string @name) =>
        {
            Vectors.MpvGetPropertyString = FunctionResolver.GetFunctionDelegate<Vectors.MpvGetPropertyStringDelegate>("libmpv", "mpv_get_property_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvGetPropertyString(@ctx, @name);
        };
        
        Vectors.MpvGetTimeUs = (MpvHandle* @ctx) =>
        {
            Vectors.MpvGetTimeUs = FunctionResolver.GetFunctionDelegate<Vectors.MpvGetTimeUsDelegate>("libmpv", "mpv_get_time_us", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvGetTimeUs(@ctx);
        };
        
        Vectors.MpvGetWakeupPipe = (MpvHandle* @ctx) =>
        {
            Vectors.MpvGetWakeupPipe = FunctionResolver.GetFunctionDelegate<Vectors.MpvGetWakeupPipeDelegate>("libmpv", "mpv_get_wakeup_pipe", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvGetWakeupPipe(@ctx);
        };
        
        Vectors.MpvHookAdd = (MpvHandle* @ctx, ulong @reply_userdata, string @name, int @priority) =>
        {
            Vectors.MpvHookAdd = FunctionResolver.GetFunctionDelegate<Vectors.MpvHookAddDelegate>("libmpv", "mpv_hook_add", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvHookAdd(@ctx, @reply_userdata, @name, @priority);
        };
        
        Vectors.MpvHookContinue = (MpvHandle* @ctx, ulong @id) =>
        {
            Vectors.MpvHookContinue = FunctionResolver.GetFunctionDelegate<Vectors.MpvHookContinueDelegate>("libmpv", "mpv_hook_continue", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvHookContinue(@ctx, @id);
        };
        
        Vectors.MpvInitialize = (MpvHandle* @ctx) =>
        {
            Vectors.MpvInitialize = FunctionResolver.GetFunctionDelegate<Vectors.MpvInitializeDelegate>("libmpv", "mpv_initialize", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvInitialize(@ctx);
        };
        
        Vectors.MpvLavcSetJavaVm = (IntPtr @jvm, IntPtr @logCtx) =>
        {
            Vectors.MpvLavcSetJavaVm = FunctionResolver.GetFunctionDelegate<Vectors.MpvLavcSetJavaVmDelegate>("libavcodec", "av_jni_set_java_vm", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvLavcSetJavaVm(@jvm, @logCtx);
        };
        
        Vectors.MpvLoadConfigFile = (MpvHandle* @ctx, string @filename) =>
        {
            Vectors.MpvLoadConfigFile = FunctionResolver.GetFunctionDelegate<Vectors.MpvLoadConfigFileDelegate>("libmpv", "mpv_load_config_file", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvLoadConfigFile(@ctx, @filename);
        };
        
        Vectors.MpvObserveProperty = (MpvHandle* @mpv, ulong @reply_userdata, string @name, MpvFormat @format) =>
        {
            Vectors.MpvObserveProperty = FunctionResolver.GetFunctionDelegate<Vectors.MpvObservePropertyDelegate>("libmpv", "mpv_observe_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvObserveProperty(@mpv, @reply_userdata, @name, @format);
        };
        
        Vectors.MpvRenderContextCreate = (MpvRenderContext** @res, MpvHandle* @mpv, MpvRenderParam* @params) =>
        {
            Vectors.MpvRenderContextCreate = FunctionResolver.GetFunctionDelegate<Vectors.MpvRenderContextCreateDelegate>("libmpv", "mpv_render_context_create", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvRenderContextCreate(@res, @mpv, @params);
        };
        
        Vectors.MpvRenderContextFree = (MpvRenderContext* @ctx) =>
        {
            Vectors.MpvRenderContextFree = FunctionResolver.GetFunctionDelegate<Vectors.MpvRenderContextFreeDelegate>("libmpv", "mpv_render_context_free", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvRenderContextFree(@ctx);
        };
        
        Vectors.MpvRenderContextGetInfo = (MpvRenderContext* @ctx, MpvRenderParam @param) =>
        {
            Vectors.MpvRenderContextGetInfo = FunctionResolver.GetFunctionDelegate<Vectors.MpvRenderContextGetInfoDelegate>("libmpv", "mpv_render_context_get_info", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvRenderContextGetInfo(@ctx, @param);
        };
        
        Vectors.MpvRenderContextRender = (MpvRenderContext* @ctx, MpvRenderParam* @params) =>
        {
            Vectors.MpvRenderContextRender = FunctionResolver.GetFunctionDelegate<Vectors.MpvRenderContextRenderDelegate>("libmpv", "mpv_render_context_render", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvRenderContextRender(@ctx, @params);
        };
        
        Vectors.MpvRenderContextReportSwap = (MpvRenderContext* @ctx) =>
        {
            Vectors.MpvRenderContextReportSwap = FunctionResolver.GetFunctionDelegate<Vectors.MpvRenderContextReportSwapDelegate>("libmpv", "mpv_render_context_report_swap", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvRenderContextReportSwap(@ctx);
        };
        
        Vectors.MpvRenderContextSetParameter = (MpvRenderContext* @ctx, MpvRenderParam @param) =>
        {
            Vectors.MpvRenderContextSetParameter = FunctionResolver.GetFunctionDelegate<Vectors.MpvRenderContextSetParameterDelegate>("libmpv", "mpv_render_context_set_parameter", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvRenderContextSetParameter(@ctx, @param);
        };
        
        Vectors.MpvRenderContextSetUpdateCallback = (MpvRenderContext* @ctx, MpvRenderContextSetUpdateCallbackCallbackFunc @callback, void* @callback_ctx) =>
        {
            Vectors.MpvRenderContextSetUpdateCallback = FunctionResolver.GetFunctionDelegate<Vectors.MpvRenderContextSetUpdateCallbackDelegate>("libmpv", "mpv_render_context_set_update_callback", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvRenderContextSetUpdateCallback(@ctx, @callback, @callback_ctx);
        };
        
        Vectors.MpvRenderContextUpdate = (MpvRenderContext* @ctx) =>
        {
            Vectors.MpvRenderContextUpdate = FunctionResolver.GetFunctionDelegate<Vectors.MpvRenderContextUpdateDelegate>("libmpv", "mpv_render_context_update", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvRenderContextUpdate(@ctx);
        };
        
        Vectors.MpvRequestEvent = (MpvHandle* @ctx, MpvEventId @event, int @enable) =>
        {
            Vectors.MpvRequestEvent = FunctionResolver.GetFunctionDelegate<Vectors.MpvRequestEventDelegate>("libmpv", "mpv_request_event", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvRequestEvent(@ctx, @event, @enable);
        };
        
        Vectors.MpvRequestLogMessages = (MpvHandle* @ctx, string @min_level) =>
        {
            Vectors.MpvRequestLogMessages = FunctionResolver.GetFunctionDelegate<Vectors.MpvRequestLogMessagesDelegate>("libmpv", "mpv_request_log_messages", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvRequestLogMessages(@ctx, @min_level);
        };
        
        Vectors.MpvSetOption = (MpvHandle* @ctx, string @name, MpvFormat @format, void* @data) =>
        {
            Vectors.MpvSetOption = FunctionResolver.GetFunctionDelegate<Vectors.MpvSetOptionDelegate>("libmpv", "mpv_set_option", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvSetOption(@ctx, @name, @format, @data);
        };
        
        Vectors.MpvSetOptionString = (MpvHandle* @ctx, string @name, string @data) =>
        {
            Vectors.MpvSetOptionString = FunctionResolver.GetFunctionDelegate<Vectors.MpvSetOptionStringDelegate>("libmpv", "mpv_set_option_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvSetOptionString(@ctx, @name, @data);
        };
        
        Vectors.MpvSetProperty = (MpvHandle* @ctx, string @name, MpvFormat @format, void* @data) =>
        {
            Vectors.MpvSetProperty = FunctionResolver.GetFunctionDelegate<Vectors.MpvSetPropertyDelegate>("libmpv", "mpv_set_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvSetProperty(@ctx, @name, @format, @data);
        };
        
        Vectors.MpvSetPropertyAsync = (MpvHandle* @ctx, ulong @reply_userdata, string @name, MpvFormat @format, void* @data) =>
        {
            Vectors.MpvSetPropertyAsync = FunctionResolver.GetFunctionDelegate<Vectors.MpvSetPropertyAsyncDelegate>("libmpv", "mpv_set_property_async", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvSetPropertyAsync(@ctx, @reply_userdata, @name, @format, @data);
        };
        
        Vectors.MpvSetPropertyString = (MpvHandle* @ctx, string @name, string @data) =>
        {
            Vectors.MpvSetPropertyString = FunctionResolver.GetFunctionDelegate<Vectors.MpvSetPropertyStringDelegate>("libmpv", "mpv_set_property_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvSetPropertyString(@ctx, @name, @data);
        };
        
        Vectors.MpvSetWakeupCallback = (MpvHandle* @ctx, MpvSetWakeupCallbackCbFunc @cb, void* @d) =>
        {
            Vectors.MpvSetWakeupCallback = FunctionResolver.GetFunctionDelegate<Vectors.MpvSetWakeupCallbackDelegate>("libmpv", "mpv_set_wakeup_callback", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvSetWakeupCallback(@ctx, @cb, @d);
        };
        
        Vectors.MpvStreamCbAddRo = (MpvHandle* @ctx, string @protocol, void* @user_data, MpvStreamCbAddRoOpenFnFunc @open_fn) =>
        {
            Vectors.MpvStreamCbAddRo = FunctionResolver.GetFunctionDelegate<Vectors.MpvStreamCbAddRoDelegate>("libmpv", "mpv_stream_cb_add_ro", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvStreamCbAddRo(@ctx, @protocol, @user_data, @open_fn);
        };
        
        Vectors.MpvTerminateDestroy = (MpvHandle* @ctx) =>
        {
            Vectors.MpvTerminateDestroy = FunctionResolver.GetFunctionDelegate<Vectors.MpvTerminateDestroyDelegate>("libmpv", "mpv_terminate_destroy", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvTerminateDestroy(@ctx);
        };
        
        Vectors.MpvUnobserveProperty = (MpvHandle* @mpv, ulong @registered_reply_userdata) =>
        {
            Vectors.MpvUnobserveProperty = FunctionResolver.GetFunctionDelegate<Vectors.MpvUnobservePropertyDelegate>("libmpv", "mpv_unobserve_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvUnobserveProperty(@mpv, @registered_reply_userdata);
        };
        
        Vectors.MpvWaitAsyncRequests = (MpvHandle* @ctx) =>
        {
            Vectors.MpvWaitAsyncRequests = FunctionResolver.GetFunctionDelegate<Vectors.MpvWaitAsyncRequestsDelegate>("libmpv", "mpv_wait_async_requests", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvWaitAsyncRequests(@ctx);
        };
        
        Vectors.MpvWaitEvent = (MpvHandle* @ctx, double @timeout) =>
        {
            Vectors.MpvWaitEvent = FunctionResolver.GetFunctionDelegate<Vectors.MpvWaitEventDelegate>("libmpv", "mpv_wait_event", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return Vectors.MpvWaitEvent(@ctx, @timeout);
        };
        
        Vectors.MpvWakeup = (MpvHandle* @ctx) =>
        {
            Vectors.MpvWakeup = FunctionResolver.GetFunctionDelegate<Vectors.MpvWakeupDelegate>("libmpv", "mpv_wakeup", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            Vectors.MpvWakeup(@ctx);
        };
        
    }
}
