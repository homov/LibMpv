using System;
using System.Security;
using System.Runtime.InteropServices;

namespace LibMpv.Client;

public static unsafe partial class vectors
{
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_abort_async_command_delegate(mpv_handle* @ctx, ulong @reply_userdata);
    public static mpv_abort_async_command_delegate mpv_abort_async_command;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong mpv_client_api_version_delegate();
    public static mpv_client_api_version_delegate mpv_client_api_version;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long mpv_client_id_delegate(mpv_handle* @ctx);
    public static mpv_client_id_delegate mpv_client_id;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string mpv_client_name_delegate(mpv_handle* @ctx);
    public static mpv_client_name_delegate mpv_client_name;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_command_delegate(mpv_handle* @ctx, byte** @args);
    public static mpv_command_delegate mpv_command;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_command_async_delegate(mpv_handle* @ctx, ulong @reply_userdata, byte** @args);
    public static mpv_command_async_delegate mpv_command_async;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_command_node_delegate(mpv_handle* @ctx, mpv_node* @args, mpv_node* @result);
    public static mpv_command_node_delegate mpv_command_node;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_command_node_async_delegate(mpv_handle* @ctx, ulong @reply_userdata, mpv_node* @args);
    public static mpv_command_node_async_delegate mpv_command_node_async;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_command_ret_delegate(mpv_handle* @ctx, byte** @args, mpv_node* @result);
    public static mpv_command_ret_delegate mpv_command_ret;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_command_string_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @args);
    public static mpv_command_string_delegate mpv_command_string;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate mpv_handle* mpv_create_delegate();
    public static mpv_create_delegate mpv_create;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate mpv_handle* mpv_create_client_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static mpv_create_client_delegate mpv_create_client;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate mpv_handle* mpv_create_weak_client_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static mpv_create_weak_client_delegate mpv_create_weak_client;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_del_property_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static mpv_del_property_delegate mpv_del_property;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_destroy_delegate(mpv_handle* @ctx);
    public static mpv_destroy_delegate mpv_destroy;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string mpv_error_string_delegate(int @error);
    public static mpv_error_string_delegate mpv_error_string;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string mpv_event_name_delegate(mpv_event_id @event);
    public static mpv_event_name_delegate mpv_event_name;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_event_to_node_delegate(mpv_node* @dst, mpv_event* @src);
    public static mpv_event_to_node_delegate mpv_event_to_node;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_free_delegate(void* @data);
    public static mpv_free_delegate mpv_free;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_free_node_contents_delegate(mpv_node* @node);
    public static mpv_free_node_contents_delegate mpv_free_node_contents;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_get_property_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, mpv_format @format, void* @data);
    public static mpv_get_property_delegate mpv_get_property;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_get_property_async_delegate(mpv_handle* @ctx, ulong @reply_userdata,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, mpv_format @format);
    public static mpv_get_property_async_delegate mpv_get_property_async;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* mpv_get_property_osd_string_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static mpv_get_property_osd_string_delegate mpv_get_property_osd_string;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* mpv_get_property_string_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name);
    public static mpv_get_property_string_delegate mpv_get_property_string;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long mpv_get_time_us_delegate(mpv_handle* @ctx);
    public static mpv_get_time_us_delegate mpv_get_time_us;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_get_wakeup_pipe_delegate(mpv_handle* @ctx);
    public static mpv_get_wakeup_pipe_delegate mpv_get_wakeup_pipe;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_hook_add_delegate(mpv_handle* @ctx, ulong @reply_userdata,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, int @priority);
    public static mpv_hook_add_delegate mpv_hook_add;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_hook_continue_delegate(mpv_handle* @ctx, ulong @id);
    public static mpv_hook_continue_delegate mpv_hook_continue;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_initialize_delegate(mpv_handle* @ctx);
    public static mpv_initialize_delegate mpv_initialize;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_load_config_file_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @filename);
    public static mpv_load_config_file_delegate mpv_load_config_file;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_observe_property_delegate(mpv_handle* @mpv, ulong @reply_userdata,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, mpv_format @format);
    public static mpv_observe_property_delegate mpv_observe_property;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_render_context_create_delegate(mpv_render_context** @res, mpv_handle* @mpv, mpv_render_param* @params);
    public static mpv_render_context_create_delegate mpv_render_context_create;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_render_context_free_delegate(mpv_render_context* @ctx);
    public static mpv_render_context_free_delegate mpv_render_context_free;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_render_context_get_info_delegate(mpv_render_context* @ctx, mpv_render_param @param);
    public static mpv_render_context_get_info_delegate mpv_render_context_get_info;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_render_context_render_delegate(mpv_render_context* @ctx, mpv_render_param* @params);
    public static mpv_render_context_render_delegate mpv_render_context_render;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_render_context_report_swap_delegate(mpv_render_context* @ctx);
    public static mpv_render_context_report_swap_delegate mpv_render_context_report_swap;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_render_context_set_parameter_delegate(mpv_render_context* @ctx, mpv_render_param @param);
    public static mpv_render_context_set_parameter_delegate mpv_render_context_set_parameter;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_render_context_set_update_callback_delegate(mpv_render_context* @ctx, mpv_render_context_set_update_callback_callback_func @callback, void* @callback_ctx);
    public static mpv_render_context_set_update_callback_delegate mpv_render_context_set_update_callback;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong mpv_render_context_update_delegate(mpv_render_context* @ctx);
    public static mpv_render_context_update_delegate mpv_render_context_update;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_request_event_delegate(mpv_handle* @ctx, mpv_event_id @event, int @enable);
    public static mpv_request_event_delegate mpv_request_event;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_request_log_messages_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @min_level);
    public static mpv_request_log_messages_delegate mpv_request_log_messages;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_set_option_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, mpv_format @format, void* @data);
    public static mpv_set_option_delegate mpv_set_option;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_set_option_string_delegate(mpv_handle* @ctx,     
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
    public static mpv_set_option_string_delegate mpv_set_option_string;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_set_property_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, mpv_format @format, void* @data);
    public static mpv_set_property_delegate mpv_set_property;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_set_property_async_delegate(mpv_handle* @ctx, ulong @reply_userdata,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @name, mpv_format @format, void* @data);
    public static mpv_set_property_async_delegate mpv_set_property_async;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_set_property_string_delegate(mpv_handle* @ctx,     
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
    public static mpv_set_property_string_delegate mpv_set_property_string;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_set_wakeup_callback_delegate(mpv_handle* @ctx, mpv_set_wakeup_callback_cb_func @cb, void* @d);
    public static mpv_set_wakeup_callback_delegate mpv_set_wakeup_callback;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_stream_cb_add_ro_delegate(mpv_handle* @ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
    #endif
    string @protocol, void* @user_data, mpv_stream_cb_add_ro_open_fn_func @open_fn);
    public static mpv_stream_cb_add_ro_delegate mpv_stream_cb_add_ro;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_terminate_destroy_delegate(mpv_handle* @ctx);
    public static mpv_terminate_destroy_delegate mpv_terminate_destroy;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int mpv_unobserve_property_delegate(mpv_handle* @mpv, ulong @registered_reply_userdata);
    public static mpv_unobserve_property_delegate mpv_unobserve_property;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_wait_async_requests_delegate(mpv_handle* @ctx);
    public static mpv_wait_async_requests_delegate mpv_wait_async_requests;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate mpv_event* mpv_wait_event_delegate(mpv_handle* @ctx, double @timeout);
    public static mpv_wait_event_delegate mpv_wait_event;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void mpv_wakeup_delegate(mpv_handle* @ctx);
    public static mpv_wakeup_delegate mpv_wakeup;
    
}
