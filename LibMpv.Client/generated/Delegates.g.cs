using System;
using System.Runtime.InteropServices;

namespace LibMpv.Client;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void* mpv_opengl_init_params_get_proc_address (void* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
public unsafe struct mpv_opengl_init_params_get_proc_address_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_opengl_init_params_get_proc_address_func(mpv_opengl_init_params_get_proc_address func) => new mpv_opengl_init_params_get_proc_address_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void mpv_render_context_set_update_callback_callback (void* @cb_ctx);
public unsafe struct mpv_render_context_set_update_callback_callback_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_render_context_set_update_callback_callback_func(mpv_render_context_set_update_callback_callback func) => new mpv_render_context_set_update_callback_callback_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void mpv_set_wakeup_callback_cb (void* @d);
public unsafe struct mpv_set_wakeup_callback_cb_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_set_wakeup_callback_cb_func(mpv_set_wakeup_callback_cb func) => new mpv_set_wakeup_callback_cb_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate int mpv_stream_cb_add_ro_open_fn (void* @user_data, byte* @uri, mpv_stream_cb_info* @info);
public unsafe struct mpv_stream_cb_add_ro_open_fn_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_stream_cb_add_ro_open_fn_func(mpv_stream_cb_add_ro_open_fn func) => new mpv_stream_cb_add_ro_open_fn_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void mpv_stream_cb_info_cancel_fn (void* @cookie);
public unsafe struct mpv_stream_cb_info_cancel_fn_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_stream_cb_info_cancel_fn_func(mpv_stream_cb_info_cancel_fn func) => new mpv_stream_cb_info_cancel_fn_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void mpv_stream_cb_info_close_fn (void* @cookie);
public unsafe struct mpv_stream_cb_info_close_fn_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_stream_cb_info_close_fn_func(mpv_stream_cb_info_close_fn func) => new mpv_stream_cb_info_close_fn_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate long mpv_stream_cb_info_read_fn (void* @cookie, byte* @buf, ulong @nbytes);
public unsafe struct mpv_stream_cb_info_read_fn_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_stream_cb_info_read_fn_func(mpv_stream_cb_info_read_fn func) => new mpv_stream_cb_info_read_fn_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate long mpv_stream_cb_info_seek_fn (void* @cookie, long @offset);
public unsafe struct mpv_stream_cb_info_seek_fn_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_stream_cb_info_seek_fn_func(mpv_stream_cb_info_seek_fn func) => new mpv_stream_cb_info_seek_fn_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate long mpv_stream_cb_info_size_fn (void* @cookie);
public unsafe struct mpv_stream_cb_info_size_fn_func
{
    public IntPtr Pointer;
    public static implicit operator mpv_stream_cb_info_size_fn_func(mpv_stream_cb_info_size_fn func) => new mpv_stream_cb_info_size_fn_func { Pointer = func == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(func) };
}

