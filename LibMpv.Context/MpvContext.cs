using System.Runtime.CompilerServices;
using System.Text;
using static LibMpv.Client.libmpv;

namespace LibMpv.Client;

public enum MpvEventLoop
{
    Default,
    Thread,
    Weak
}

public unsafe partial class MpvContext : IDisposable
{
    public MpvContext():this(MpvEventLoop.Default)
    {
    }
    
    public MpvContext(MpvEventLoop mpvEventLoop)
    {
        ctx = mpv_create();
        
        if (ctx == null)
            throw new MpvException("Unable to create mpv_context. Currently, this can happen in the following situations - out of memory or LC_NUMERIC is not set to \"C\"");
        
        var code = mpv_initialize(ctx);
        CheckCode(code);

        InitEventHandlers();

        if (mpvEventLoop == MpvEventLoop.Default)
            eventLoop = new MpvSimpleEventLoop(ctx, this.HandleEvent);
        else if (mpvEventLoop == MpvEventLoop.Thread)
            eventLoop = new MpvThreadEventLoop(ctx, this.HandleEvent);
        else
            eventLoop = new MpvWeakEventLoop(ctx, this.HandleEvent);

        eventLoop.Start();
    }

    ~MpvContext()
    {
        Dispose(false);
    }

    public void RequestLogMessages(string level)
    {
        CheckDisposed();
        mpv_request_log_messages(ctx, level);
    }

    #region Properties
    public string GetPropertyString(string name)
    {
        CheckDisposed();
        var value = mpv_get_property_string(ctx, name);
        return UTF8Marshaler.FromNative(Encoding.UTF8, value);
    }

    public void SetPropertyString(string name, string newValue)
    {
        CheckDisposed();
        int code = mpv_set_property_string(ctx, name, newValue);
        CheckCode(code);
    }

    public bool GetPropertyFlag(string name)
    {
        CheckDisposed();
        int code;
        var value= new int[1] { 0 };
        fixed(int* valuePtr = value)
        {
            code = mpv_get_property(ctx, name, mpv_format.MPV_FORMAT_FLAG, valuePtr );
        }
        CheckCode(code);
        return value[0] == 1 ? true : false;
    }

    public void SetPropertyFlag(string name, bool newValue)
    {
        CheckDisposed();
        int code;
        var value = new int[1] { newValue ? 1:0 };
        fixed (int* valuePtr = value)
        {
            code = mpv_set_property(ctx, name, mpv_format.MPV_FORMAT_FLAG, valuePtr);
        }
        CheckCode(code);
    }

    public long GetPropertyLong(string name)
    {
        CheckDisposed();
        int code;
        var value = new long[1] { 0 };
        fixed (long* valuePtr = value)
        {
            code = mpv_get_property(ctx, name, mpv_format.MPV_FORMAT_INT64, valuePtr);
        }
        CheckCode(code);
        return value[0];
    }

    public void SetPropertyLong(string name, long newValue)
    {
        CheckDisposed();
        int code;
        var value = new long[1] { newValue };
        fixed (long* valuePtr = value)
        {
            code = mpv_set_property(ctx, name, mpv_format.MPV_FORMAT_INT64, valuePtr);
        }
        CheckCode(code);
    }

    public double GetPropertyDouble(string name)
    {
        CheckDisposed();
        int code;
        var value = new double[1] { 0 };
        fixed (double* valuePtr = value)
        {
            code = mpv_get_property(ctx, name, mpv_format.MPV_FORMAT_DOUBLE, valuePtr);
        }
        CheckCode(code);
        return value[0];
    }

    public void SetPropertyDouble(string name, double newValue)
    {
        CheckDisposed();
        int code;
        var value = new double[1] { 0 };
        fixed (double* valuePtr = value)
        {
            code = mpv_set_property(ctx, name, mpv_format.MPV_FORMAT_DOUBLE, valuePtr);
        }
        CheckCode(code);
    }

    public void ObserveProperty(string name, mpv_format format, ulong userData)
    {
        CheckDisposed();
        int code = mpv_observe_property(ctx, userData, name, format);
        CheckCode(code);
    }

    public void UnobserveProperty(ulong userData)
    {
        CheckDisposed();
        int code = mpv_unobserve_property(ctx, userData);
        CheckCode(code);
    }

    #endregion

    public void Command(params string[] args)
    {
        if (args.Length == 0)
            throw new ArgumentException("Missing arguments.", nameof(args));
        
        CheckDisposed();

        using var helper = new MarshalHelper();
        int code = mpv_command(ctx, (byte**)helper.CStringArrayForManagedUTF8StringArray(args));
        
        CheckCode(code);
    }


    public void CommandAsync(ulong userData, params string[] args)
    {
        if (args.Length == 0)
            throw new ArgumentException("Missing arguments.", nameof(args));

        CheckDisposed();

        using var helper = new MarshalHelper();
        int code = mpv_command_async(ctx, userData, (byte**)helper.CStringArrayForManagedUTF8StringArray(args));

        CheckCode(code);
    }

    public void SetOptionString(string name, string data)
    {
        CheckDisposed();
        int code = mpv_set_option_string(ctx, name, data);
        CheckCode(code);
    }

    public void RequestEvent(mpv_event_id @event, bool enabled)
    {
        CheckDisposed();
        int code = mpv_request_event(ctx, @event, enabled ? 1 : 0);
        CheckCode(code);
    }

    public string EventName(mpv_event_id @event)
    {
        return mpv_event_name(@event);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            StopRendering();
            eventLoop.Stop();
            if (eventLoop is IDisposable disposable)
                disposable.Dispose();
            mpv_terminate_destroy(ctx);
            disposed = true;
        }
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int CheckCode(int code)
    {
        if (code >= 0)
            return code;
        throw MpvException.FromCode(code);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void CheckDisposed()
    {
        if (disposed) throw new ObjectDisposedException(nameof(MpvContext));
    }

    protected bool disposed = false;
    protected mpv_handle* ctx;
    private IEventLoop eventLoop;
}
