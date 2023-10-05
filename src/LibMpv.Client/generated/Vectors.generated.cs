using System;
using System.Security;
using System.Runtime.InteropServices;

namespace LibMpv.Client;

public static unsafe partial class Vectors
{
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvAbortAsyncCommandDelegate(MpvHandle* @ctx, ulong @reply_userdata);
    public static MpvAbortAsyncCommandDelegate MpvAbortAsyncCommand;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong MpvClientApiVersionDelegate();
    public static MpvClientApiVersionDelegate MpvClientApiVersion;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long MpvClientIdDelegate(MpvHandle* @ctx);
    public static MpvClientIdDelegate MpvClientId;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string MpvClientNameDelegate(MpvHandle* @ctx);
    public static MpvClientNameDelegate MpvClientName;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvCommandDelegate(MpvHandle* @ctx, byte** @args);
    public static MpvCommandDelegate MpvCommand;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvCommandAsyncDelegate(MpvHandle* @ctx, ulong @reply_userdata, byte** @args);
    public static MpvCommandAsyncDelegate MpvCommandAsync;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvCommandNodeDelegate(MpvHandle* @ctx, MpvNode* @args, MpvNode* @result);
    public static MpvCommandNodeDelegate MpvCommandNode;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvCommandNodeAsyncDelegate(MpvHandle* @ctx, ulong @reply_userdata, MpvNode* @args);
    public static MpvCommandNodeAsyncDelegate MpvCommandNodeAsync;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvCommandRetDelegate(MpvHandle* @ctx, byte** @args, MpvNode* @result);
    public static MpvCommandRetDelegate MpvCommandRet;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvCommandStringDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @args);
    public static MpvCommandStringDelegate MpvCommandString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MpvHandle* MpvCreateDelegate();
    public static MpvCreateDelegate MpvCreate;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MpvHandle* MpvCreateClientDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static MpvCreateClientDelegate MpvCreateClient;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MpvHandle* MpvCreateWeakClientDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static MpvCreateWeakClientDelegate MpvCreateWeakClient;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvDelPropertyDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static MpvDelPropertyDelegate MpvDelProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvDestroyDelegate(MpvHandle* @ctx);
    public static MpvDestroyDelegate MpvDestroy;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string MpvErrorStringDelegate(int @error);
    public static MpvErrorStringDelegate MpvErrorString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string MpvEventNameDelegate(MpvEventId @event);
    public static MpvEventNameDelegate MpvEventName;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvEventToNodeDelegate(MpvNode* @dst, MpvEvent* @src);
    public static MpvEventToNodeDelegate MpvEventToNode;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvFreeDelegate(void* @data);
    public static MpvFreeDelegate MpvFree;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvFreeNodeContentsDelegate(MpvNode* @node);
    public static MpvFreeNodeContentsDelegate MpvFreeNodeContents;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvGetPropertyDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, MpvFormat @format, void* @data);
    public static MpvGetPropertyDelegate MpvGetProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvGetPropertyAsyncDelegate(MpvHandle* @ctx, ulong @reply_userdata,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, MpvFormat @format);
    public static MpvGetPropertyAsyncDelegate MpvGetPropertyAsync;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* MpvGetPropertyOsdStringDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static MpvGetPropertyOsdStringDelegate MpvGetPropertyOsdString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* MpvGetPropertyStringDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static MpvGetPropertyStringDelegate MpvGetPropertyString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long MpvGetTimeUsDelegate(MpvHandle* @ctx);
    public static MpvGetTimeUsDelegate MpvGetTimeUs;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvGetWakeupPipeDelegate(MpvHandle* @ctx);
    public static MpvGetWakeupPipeDelegate MpvGetWakeupPipe;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvHookAddDelegate(MpvHandle* @ctx, ulong @reply_userdata,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, int @priority);
    public static MpvHookAddDelegate MpvHookAdd;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvHookContinueDelegate(MpvHandle* @ctx, ulong @id);
    public static MpvHookContinueDelegate MpvHookContinue;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvInitializeDelegate(MpvHandle* @ctx);
    public static MpvInitializeDelegate MpvInitialize;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvLavcSetJavaVmDelegate(IntPtr @jvm, IntPtr @logCtx);
    public static MpvLavcSetJavaVmDelegate MpvLavcSetJavaVm;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvLoadConfigFileDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @filename);
    public static MpvLoadConfigFileDelegate MpvLoadConfigFile;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvObservePropertyDelegate(MpvHandle* @mpv, ulong @reply_userdata,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, MpvFormat @format);
    public static MpvObservePropertyDelegate MpvObserveProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvRenderContextCreateDelegate(MpvRenderContext** @res, MpvHandle* @mpv, MpvRenderParam* @params);
    public static MpvRenderContextCreateDelegate MpvRenderContextCreate;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvRenderContextFreeDelegate(MpvRenderContext* @ctx);
    public static MpvRenderContextFreeDelegate MpvRenderContextFree;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvRenderContextGetInfoDelegate(MpvRenderContext* @ctx, MpvRenderParam @param);
    public static MpvRenderContextGetInfoDelegate MpvRenderContextGetInfo;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvRenderContextRenderDelegate(MpvRenderContext* @ctx, MpvRenderParam* @params);
    public static MpvRenderContextRenderDelegate MpvRenderContextRender;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvRenderContextReportSwapDelegate(MpvRenderContext* @ctx);
    public static MpvRenderContextReportSwapDelegate MpvRenderContextReportSwap;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvRenderContextSetParameterDelegate(MpvRenderContext* @ctx, MpvRenderParam @param);
    public static MpvRenderContextSetParameterDelegate MpvRenderContextSetParameter;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvRenderContextSetUpdateCallbackDelegate(MpvRenderContext* @ctx, MpvRenderContextSetUpdateCallbackCallbackFunc @callback, void* @callback_ctx);
    public static MpvRenderContextSetUpdateCallbackDelegate MpvRenderContextSetUpdateCallback;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong MpvRenderContextUpdateDelegate(MpvRenderContext* @ctx);
    public static MpvRenderContextUpdateDelegate MpvRenderContextUpdate;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvRequestEventDelegate(MpvHandle* @ctx, MpvEventId @event, int @enable);
    public static MpvRequestEventDelegate MpvRequestEvent;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvRequestLogMessagesDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @min_level);
    public static MpvRequestLogMessagesDelegate MpvRequestLogMessages;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvSetOptionDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, MpvFormat @format, void* @data);
    public static MpvSetOptionDelegate MpvSetOption;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvSetOptionStringDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @data);
    public static MpvSetOptionStringDelegate MpvSetOptionString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvSetPropertyDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, MpvFormat @format, void* @data);
    public static MpvSetPropertyDelegate MpvSetProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvSetPropertyAsyncDelegate(MpvHandle* @ctx, ulong @reply_userdata,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, MpvFormat @format, void* @data);
    public static MpvSetPropertyAsyncDelegate MpvSetPropertyAsync;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvSetPropertyStringDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @data);
    public static MpvSetPropertyStringDelegate MpvSetPropertyString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvSetWakeupCallbackDelegate(MpvHandle* @ctx, MpvSetWakeupCallbackCbFunc @cb, void* @d);
    public static MpvSetWakeupCallbackDelegate MpvSetWakeupCallback;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvStreamCbAddRoDelegate(MpvHandle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @protocol, void* @user_data, MpvStreamCbAddRoOpenFnFunc @open_fn);
    public static MpvStreamCbAddRoDelegate MpvStreamCbAddRo;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvTerminateDestroyDelegate(MpvHandle* @ctx);
    public static MpvTerminateDestroyDelegate MpvTerminateDestroy;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MpvUnobservePropertyDelegate(MpvHandle* @mpv, ulong @registered_reply_userdata);
    public static MpvUnobservePropertyDelegate MpvUnobserveProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvWaitAsyncRequestsDelegate(MpvHandle* @ctx);
    public static MpvWaitAsyncRequestsDelegate MpvWaitAsyncRequests;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MpvEvent* MpvWaitEventDelegate(MpvHandle* @ctx, double @timeout);
    public static MpvWaitEventDelegate MpvWaitEvent;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MpvWakeupDelegate(MpvHandle* @ctx);
    public static MpvWakeupDelegate MpvWakeup;
    
}
