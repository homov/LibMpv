using System;
using System.Runtime.InteropServices;

namespace LibMpv.Client;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void* MpvOpenglInitParamsGetProcAddress (void* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
public unsafe struct Mpvopenglinitparamsgetprocaddressfunc
{
    public IntPtr Pointer;
    public static implicit operator Mpvopenglinitparamsgetprocaddressfunc(MpvOpenglInitParamsGetProcAddress func) => new Mpvopenglinitparamsgetprocaddressfunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void MpvRenderContextSetUpdateCallbackCallback (void* @cb_ctx);
public unsafe struct MpvRenderContextSetUpdateCallbackCallbackFunc
{
    public IntPtr Pointer;
    public static implicit operator MpvRenderContextSetUpdateCallbackCallbackFunc(MpvRenderContextSetUpdateCallbackCallback func) => new MpvRenderContextSetUpdateCallbackCallbackFunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void MpvSetWakeupCallbackCb (void* @d);
public unsafe struct MpvSetWakeupCallbackCbFunc
{
    public IntPtr Pointer;
    public static implicit operator MpvSetWakeupCallbackCbFunc(MpvSetWakeupCallbackCb func) => new MpvSetWakeupCallbackCbFunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate int MpvStreamCbAddRoOpenFn (void* @user_data, byte* @uri, MpvStreamCbInfo* @info);
public unsafe struct MpvStreamCbAddRoOpenFnFunc
{
    public IntPtr Pointer;
    public static implicit operator MpvStreamCbAddRoOpenFnFunc(MpvStreamCbAddRoOpenFn func) => new MpvStreamCbAddRoOpenFnFunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void MpvStreamCbInfoCancelFn (void* @cookie);
public unsafe struct Mpvstreamcbinfocancelfnfunc
{
    public IntPtr Pointer;
    public static implicit operator Mpvstreamcbinfocancelfnfunc(MpvStreamCbInfoCancelFn func) => new Mpvstreamcbinfocancelfnfunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void MpvStreamCbInfoCloseFn (void* @cookie);
public unsafe struct Mpvstreamcbinfoclosefnfunc
{
    public IntPtr Pointer;
    public static implicit operator Mpvstreamcbinfoclosefnfunc(MpvStreamCbInfoCloseFn func) => new Mpvstreamcbinfoclosefnfunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate long MpvStreamCbInfoReadFn (void* @cookie, byte* @buf, ulong @nbytes);
public unsafe struct Mpvstreamcbinforeadfnfunc
{
    public IntPtr Pointer;
    public static implicit operator Mpvstreamcbinforeadfnfunc(MpvStreamCbInfoReadFn func) => new Mpvstreamcbinforeadfnfunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate long MpvStreamCbInfoSeekFn (void* @cookie, long @offset);
public unsafe struct Mpvstreamcbinfoseekfnfunc
{
    public IntPtr Pointer;
    public static implicit operator Mpvstreamcbinfoseekfnfunc(MpvStreamCbInfoSeekFn func) => new Mpvstreamcbinfoseekfnfunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate long MpvStreamCbInfoSizeFn (void* @cookie);
public unsafe struct Mpvstreamcbinfosizefnfunc
{
    public IntPtr Pointer;
    public static implicit operator Mpvstreamcbinfosizefnfunc(MpvStreamCbInfoSizeFn func) => new Mpvstreamcbinfosizefnfunc { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

