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
        
        vectors.mpv_abort_async_command = (mpv_handle* @ctx, ulong @reply_userdata) =>
        {
            vectors.mpv_abort_async_command = FunctionResolver.GetFunctionDelegate<vectors.mpv_abort_async_command_delegate>("libmpv", "mpv_abort_async_command", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_abort_async_command(@ctx, @reply_userdata);
        };
        
        vectors.mpv_client_api_version = () =>
        {
            vectors.mpv_client_api_version = FunctionResolver.GetFunctionDelegate<vectors.mpv_client_api_version_delegate>("libmpv", "mpv_client_api_version", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_client_api_version();
        };
        
        vectors.mpv_client_id = (mpv_handle* @ctx) =>
        {
            vectors.mpv_client_id = FunctionResolver.GetFunctionDelegate<vectors.mpv_client_id_delegate>("libmpv", "mpv_client_id", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_client_id(@ctx);
        };
        
        vectors.mpv_client_name = (mpv_handle* @ctx) =>
        {
            vectors.mpv_client_name = FunctionResolver.GetFunctionDelegate<vectors.mpv_client_name_delegate>("libmpv", "mpv_client_name", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_client_name(@ctx);
        };
        
        vectors.mpv_command = (mpv_handle* @ctx, byte** @args) =>
        {
            vectors.mpv_command = FunctionResolver.GetFunctionDelegate<vectors.mpv_command_delegate>("libmpv", "mpv_command", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_command(@ctx, @args);
        };
        
        vectors.mpv_command_async = (mpv_handle* @ctx, ulong @reply_userdata, byte** @args) =>
        {
            vectors.mpv_command_async = FunctionResolver.GetFunctionDelegate<vectors.mpv_command_async_delegate>("libmpv", "mpv_command_async", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_command_async(@ctx, @reply_userdata, @args);
        };
        
        vectors.mpv_command_node = (mpv_handle* @ctx, mpv_node* @args, mpv_node* @result) =>
        {
            vectors.mpv_command_node = FunctionResolver.GetFunctionDelegate<vectors.mpv_command_node_delegate>("libmpv", "mpv_command_node", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_command_node(@ctx, @args, @result);
        };
        
        vectors.mpv_command_node_async = (mpv_handle* @ctx, ulong @reply_userdata, mpv_node* @args) =>
        {
            vectors.mpv_command_node_async = FunctionResolver.GetFunctionDelegate<vectors.mpv_command_node_async_delegate>("libmpv", "mpv_command_node_async", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_command_node_async(@ctx, @reply_userdata, @args);
        };
        
        vectors.mpv_command_ret = (mpv_handle* @ctx, byte** @args, mpv_node* @result) =>
        {
            vectors.mpv_command_ret = FunctionResolver.GetFunctionDelegate<vectors.mpv_command_ret_delegate>("libmpv", "mpv_command_ret", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_command_ret(@ctx, @args, @result);
        };
        
        vectors.mpv_command_string = (mpv_handle* @ctx, string @args) =>
        {
            vectors.mpv_command_string = FunctionResolver.GetFunctionDelegate<vectors.mpv_command_string_delegate>("libmpv", "mpv_command_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_command_string(@ctx, @args);
        };
        
        vectors.mpv_create = () =>
        {
            vectors.mpv_create = FunctionResolver.GetFunctionDelegate<vectors.mpv_create_delegate>("libmpv", "mpv_create", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_create();
        };
        
        vectors.mpv_create_client = (mpv_handle* @ctx, string @name) =>
        {
            vectors.mpv_create_client = FunctionResolver.GetFunctionDelegate<vectors.mpv_create_client_delegate>("libmpv", "mpv_create_client", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_create_client(@ctx, @name);
        };
        
        vectors.mpv_create_weak_client = (mpv_handle* @ctx, string @name) =>
        {
            vectors.mpv_create_weak_client = FunctionResolver.GetFunctionDelegate<vectors.mpv_create_weak_client_delegate>("libmpv", "mpv_create_weak_client", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_create_weak_client(@ctx, @name);
        };
        
        vectors.mpv_del_property = (mpv_handle* @ctx, string @name) =>
        {
            vectors.mpv_del_property = FunctionResolver.GetFunctionDelegate<vectors.mpv_del_property_delegate>("libmpv", "mpv_del_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_del_property(@ctx, @name);
        };
        
        vectors.mpv_destroy = (mpv_handle* @ctx) =>
        {
            vectors.mpv_destroy = FunctionResolver.GetFunctionDelegate<vectors.mpv_destroy_delegate>("libmpv", "mpv_destroy", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_destroy(@ctx);
        };
        
        vectors.mpv_error_string = (int @error) =>
        {
            vectors.mpv_error_string = FunctionResolver.GetFunctionDelegate<vectors.mpv_error_string_delegate>("libmpv", "mpv_error_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_error_string(@error);
        };
        
        vectors.mpv_event_name = (mpv_event_id @event) =>
        {
            vectors.mpv_event_name = FunctionResolver.GetFunctionDelegate<vectors.mpv_event_name_delegate>("libmpv", "mpv_event_name", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_event_name(@event);
        };
        
        vectors.mpv_event_to_node = (mpv_node* @dst, mpv_event* @src) =>
        {
            vectors.mpv_event_to_node = FunctionResolver.GetFunctionDelegate<vectors.mpv_event_to_node_delegate>("libmpv", "mpv_event_to_node", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_event_to_node(@dst, @src);
        };
        
        vectors.mpv_free = (void* @data) =>
        {
            vectors.mpv_free = FunctionResolver.GetFunctionDelegate<vectors.mpv_free_delegate>("libmpv", "mpv_free", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_free(@data);
        };
        
        vectors.mpv_free_node_contents = (mpv_node* @node) =>
        {
            vectors.mpv_free_node_contents = FunctionResolver.GetFunctionDelegate<vectors.mpv_free_node_contents_delegate>("libmpv", "mpv_free_node_contents", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_free_node_contents(@node);
        };
        
        vectors.mpv_get_property = (mpv_handle* @ctx, string @name, mpv_format @format, void* @data) =>
        {
            vectors.mpv_get_property = FunctionResolver.GetFunctionDelegate<vectors.mpv_get_property_delegate>("libmpv", "mpv_get_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_get_property(@ctx, @name, @format, @data);
        };
        
        vectors.mpv_get_property_async = (mpv_handle* @ctx, ulong @reply_userdata, string @name, mpv_format @format) =>
        {
            vectors.mpv_get_property_async = FunctionResolver.GetFunctionDelegate<vectors.mpv_get_property_async_delegate>("libmpv", "mpv_get_property_async", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_get_property_async(@ctx, @reply_userdata, @name, @format);
        };
        
        vectors.mpv_get_property_osd_string = (mpv_handle* @ctx, string @name) =>
        {
            vectors.mpv_get_property_osd_string = FunctionResolver.GetFunctionDelegate<vectors.mpv_get_property_osd_string_delegate>("libmpv", "mpv_get_property_osd_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_get_property_osd_string(@ctx, @name);
        };
        
        vectors.mpv_get_property_string = (mpv_handle* @ctx, string @name) =>
        {
            vectors.mpv_get_property_string = FunctionResolver.GetFunctionDelegate<vectors.mpv_get_property_string_delegate>("libmpv", "mpv_get_property_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_get_property_string(@ctx, @name);
        };
        
        vectors.mpv_get_time_us = (mpv_handle* @ctx) =>
        {
            vectors.mpv_get_time_us = FunctionResolver.GetFunctionDelegate<vectors.mpv_get_time_us_delegate>("libmpv", "mpv_get_time_us", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_get_time_us(@ctx);
        };
        
        vectors.mpv_get_wakeup_pipe = (mpv_handle* @ctx) =>
        {
            vectors.mpv_get_wakeup_pipe = FunctionResolver.GetFunctionDelegate<vectors.mpv_get_wakeup_pipe_delegate>("libmpv", "mpv_get_wakeup_pipe", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_get_wakeup_pipe(@ctx);
        };
        
        vectors.mpv_hook_add = (mpv_handle* @ctx, ulong @reply_userdata, string @name, int @priority) =>
        {
            vectors.mpv_hook_add = FunctionResolver.GetFunctionDelegate<vectors.mpv_hook_add_delegate>("libmpv", "mpv_hook_add", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_hook_add(@ctx, @reply_userdata, @name, @priority);
        };
        
        vectors.mpv_hook_continue = (mpv_handle* @ctx, ulong @id) =>
        {
            vectors.mpv_hook_continue = FunctionResolver.GetFunctionDelegate<vectors.mpv_hook_continue_delegate>("libmpv", "mpv_hook_continue", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_hook_continue(@ctx, @id);
        };
        
        vectors.mpv_initialize = (mpv_handle* @ctx) =>
        {
            vectors.mpv_initialize = FunctionResolver.GetFunctionDelegate<vectors.mpv_initialize_delegate>("libmpv", "mpv_initialize", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_initialize(@ctx);
        };
        
        vectors.mpv_load_config_file = (mpv_handle* @ctx, string @filename) =>
        {
            vectors.mpv_load_config_file = FunctionResolver.GetFunctionDelegate<vectors.mpv_load_config_file_delegate>("libmpv", "mpv_load_config_file", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_load_config_file(@ctx, @filename);
        };
        
        vectors.mpv_observe_property = (mpv_handle* @mpv, ulong @reply_userdata, string @name, mpv_format @format) =>
        {
            vectors.mpv_observe_property = FunctionResolver.GetFunctionDelegate<vectors.mpv_observe_property_delegate>("libmpv", "mpv_observe_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_observe_property(@mpv, @reply_userdata, @name, @format);
        };
        
        vectors.mpv_render_context_create = (mpv_render_context** @res, mpv_handle* @mpv, mpv_render_param* @params) =>
        {
            vectors.mpv_render_context_create = FunctionResolver.GetFunctionDelegate<vectors.mpv_render_context_create_delegate>("libmpv", "mpv_render_context_create", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_render_context_create(@res, @mpv, @params);
        };
        
        vectors.mpv_render_context_free = (mpv_render_context* @ctx) =>
        {
            vectors.mpv_render_context_free = FunctionResolver.GetFunctionDelegate<vectors.mpv_render_context_free_delegate>("libmpv", "mpv_render_context_free", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_render_context_free(@ctx);
        };
        
        vectors.mpv_render_context_get_info = (mpv_render_context* @ctx, mpv_render_param @param) =>
        {
            vectors.mpv_render_context_get_info = FunctionResolver.GetFunctionDelegate<vectors.mpv_render_context_get_info_delegate>("libmpv", "mpv_render_context_get_info", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_render_context_get_info(@ctx, @param);
        };
        
        vectors.mpv_render_context_render = (mpv_render_context* @ctx, mpv_render_param* @params) =>
        {
            vectors.mpv_render_context_render = FunctionResolver.GetFunctionDelegate<vectors.mpv_render_context_render_delegate>("libmpv", "mpv_render_context_render", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_render_context_render(@ctx, @params);
        };
        
        vectors.mpv_render_context_report_swap = (mpv_render_context* @ctx) =>
        {
            vectors.mpv_render_context_report_swap = FunctionResolver.GetFunctionDelegate<vectors.mpv_render_context_report_swap_delegate>("libmpv", "mpv_render_context_report_swap", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_render_context_report_swap(@ctx);
        };
        
        vectors.mpv_render_context_set_parameter = (mpv_render_context* @ctx, mpv_render_param @param) =>
        {
            vectors.mpv_render_context_set_parameter = FunctionResolver.GetFunctionDelegate<vectors.mpv_render_context_set_parameter_delegate>("libmpv", "mpv_render_context_set_parameter", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_render_context_set_parameter(@ctx, @param);
        };
        
        vectors.mpv_render_context_set_update_callback = (mpv_render_context* @ctx, mpv_render_context_set_update_callback_callback_func @callback, void* @callback_ctx) =>
        {
            vectors.mpv_render_context_set_update_callback = FunctionResolver.GetFunctionDelegate<vectors.mpv_render_context_set_update_callback_delegate>("libmpv", "mpv_render_context_set_update_callback", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_render_context_set_update_callback(@ctx, @callback, @callback_ctx);
        };
        
        vectors.mpv_render_context_update = (mpv_render_context* @ctx) =>
        {
            vectors.mpv_render_context_update = FunctionResolver.GetFunctionDelegate<vectors.mpv_render_context_update_delegate>("libmpv", "mpv_render_context_update", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_render_context_update(@ctx);
        };
        
        vectors.mpv_request_event = (mpv_handle* @ctx, mpv_event_id @event, int @enable) =>
        {
            vectors.mpv_request_event = FunctionResolver.GetFunctionDelegate<vectors.mpv_request_event_delegate>("libmpv", "mpv_request_event", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_request_event(@ctx, @event, @enable);
        };
        
        vectors.mpv_request_log_messages = (mpv_handle* @ctx, string @min_level) =>
        {
            vectors.mpv_request_log_messages = FunctionResolver.GetFunctionDelegate<vectors.mpv_request_log_messages_delegate>("libmpv", "mpv_request_log_messages", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_request_log_messages(@ctx, @min_level);
        };
        
        vectors.mpv_set_option = (mpv_handle* @ctx, string @name, mpv_format @format, void* @data) =>
        {
            vectors.mpv_set_option = FunctionResolver.GetFunctionDelegate<vectors.mpv_set_option_delegate>("libmpv", "mpv_set_option", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_set_option(@ctx, @name, @format, @data);
        };
        
        vectors.mpv_set_option_string = (mpv_handle* @ctx, string @name, string @data) =>
        {
            vectors.mpv_set_option_string = FunctionResolver.GetFunctionDelegate<vectors.mpv_set_option_string_delegate>("libmpv", "mpv_set_option_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_set_option_string(@ctx, @name, @data);
        };
        
        vectors.mpv_set_property = (mpv_handle* @ctx, string @name, mpv_format @format, void* @data) =>
        {
            vectors.mpv_set_property = FunctionResolver.GetFunctionDelegate<vectors.mpv_set_property_delegate>("libmpv", "mpv_set_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_set_property(@ctx, @name, @format, @data);
        };
        
        vectors.mpv_set_property_async = (mpv_handle* @ctx, ulong @reply_userdata, string @name, mpv_format @format, void* @data) =>
        {
            vectors.mpv_set_property_async = FunctionResolver.GetFunctionDelegate<vectors.mpv_set_property_async_delegate>("libmpv", "mpv_set_property_async", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_set_property_async(@ctx, @reply_userdata, @name, @format, @data);
        };
        
        vectors.mpv_set_property_string = (mpv_handle* @ctx, string @name, string @data) =>
        {
            vectors.mpv_set_property_string = FunctionResolver.GetFunctionDelegate<vectors.mpv_set_property_string_delegate>("libmpv", "mpv_set_property_string", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_set_property_string(@ctx, @name, @data);
        };
        
        vectors.mpv_set_wakeup_callback = (mpv_handle* @ctx, mpv_set_wakeup_callback_cb_func @cb, void* @d) =>
        {
            vectors.mpv_set_wakeup_callback = FunctionResolver.GetFunctionDelegate<vectors.mpv_set_wakeup_callback_delegate>("libmpv", "mpv_set_wakeup_callback", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_set_wakeup_callback(@ctx, @cb, @d);
        };
        
        vectors.mpv_stream_cb_add_ro = (mpv_handle* @ctx, string @protocol, void* @user_data, mpv_stream_cb_add_ro_open_fn_func @open_fn) =>
        {
            vectors.mpv_stream_cb_add_ro = FunctionResolver.GetFunctionDelegate<vectors.mpv_stream_cb_add_ro_delegate>("libmpv", "mpv_stream_cb_add_ro", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_stream_cb_add_ro(@ctx, @protocol, @user_data, @open_fn);
        };
        
        vectors.mpv_terminate_destroy = (mpv_handle* @ctx) =>
        {
            vectors.mpv_terminate_destroy = FunctionResolver.GetFunctionDelegate<vectors.mpv_terminate_destroy_delegate>("libmpv", "mpv_terminate_destroy", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_terminate_destroy(@ctx);
        };
        
        vectors.mpv_unobserve_property = (mpv_handle* @mpv, ulong @registered_reply_userdata) =>
        {
            vectors.mpv_unobserve_property = FunctionResolver.GetFunctionDelegate<vectors.mpv_unobserve_property_delegate>("libmpv", "mpv_unobserve_property", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_unobserve_property(@mpv, @registered_reply_userdata);
        };
        
        vectors.mpv_wait_async_requests = (mpv_handle* @ctx) =>
        {
            vectors.mpv_wait_async_requests = FunctionResolver.GetFunctionDelegate<vectors.mpv_wait_async_requests_delegate>("libmpv", "mpv_wait_async_requests", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_wait_async_requests(@ctx);
        };
        
        vectors.mpv_wait_event = (mpv_handle* @ctx, double @timeout) =>
        {
            vectors.mpv_wait_event = FunctionResolver.GetFunctionDelegate<vectors.mpv_wait_event_delegate>("libmpv", "mpv_wait_event", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            return vectors.mpv_wait_event(@ctx, @timeout);
        };
        
        vectors.mpv_wakeup = (mpv_handle* @ctx) =>
        {
            vectors.mpv_wakeup = FunctionResolver.GetFunctionDelegate<vectors.mpv_wakeup_delegate>("libmpv", "mpv_wakeup", ThrowErrorIfFunctionNotFound) ?? delegate { throw new NotSupportedException(); };
            vectors.mpv_wakeup(@ctx);
        };
        
    }
}
