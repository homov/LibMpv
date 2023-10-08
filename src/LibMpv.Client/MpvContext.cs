using System.Net.Http.Headers;
using System.Text;

namespace LibMpv.Client;

public unsafe partial class MpvContext: IDisposable
{
    public MpvContext()
    {
        handle = LibMpv.MpvCreate();
        if (handle == null)
        {
            throw new MpvException( "Unable to create mpv handle");
        }

        var result = LibMpv.MpvInitialize(handle);
        if (result < 0 )
        {
            LibMpv.MpvDestroy(handle);
            ThrowMpvError(result);
        }

        eventLoop = new(handle, HandleMpvEvent);
        eventLoop.Start();
    }

    public void SetLogLevel(MpvLogLevel logLevel)
    {
        if (disposed) return;
        var level = logLevel.ToString().ToLower();
        LibMpv.MpvRequestLogMessages(handle, level);
    }

    public void RemoveOverlay(uint id)
    {
        Command("overlay-remove", id.ToString());
    }

    public void AddOverlay(uint id, uint x, uint y, byte[] imageData, string format, uint width, uint height, uint stride)
    {
        fixed(byte* imageDataPtr = imageData)
        {
            Command(
                "overlay-add",
                id.ToString(),
                x.ToString(),
                y.ToString(),
                "&" + ((IntPtr)imageDataPtr).ToInt64().ToString(),
                format,
                width.ToString(),
                height.ToString(),
                stride.ToString()
            );
        }
    }

    public void AddOverlay(uint id, byte[] imageData, string format, uint width, uint height, uint stride)
    {
        fixed (byte* imageDataPtr = imageData)
        {
            Command(
                "overlay-add",
                id.ToString(),
                "0",
                "0",
                "&" + ((IntPtr)imageDataPtr).ToInt64().ToString(),
                format,
                width.ToString(),
                height.ToString(),
                stride.ToString()
            );
        }
    }

    public void ObserveProperty(string propertyName, Action changedAction)
    {
        if (!disposed)
        {
            lock (propertyChangedActions)
            {
                List<Action> actionList;
                if (propertyChangedActions.TryGetValue(propertyName, out actionList))
                {
                    actionList.Add(changedAction);
                }
                else
                {
                    var result = LibMpv.MpvObserveProperty(handle, 0, propertyName, MpvFormat.MpvFormatNone);
                    if (result < 0) ThrowMpvError(result);
                    actionList = new();
                    actionList.Add(changedAction);
                    propertyChangedActions.Add(propertyName, actionList);
                }
            }
        }
    }

    public void ObserveProperty(string propertyName, Action<bool> changedAction)
    {
        if (!disposed)
        {
            lock (propertyBoolChangedActions)
            {
                List<Action<bool>> actionList;
                if (propertyBoolChangedActions.TryGetValue(propertyName, out actionList))
                {
                    actionList.Add(changedAction);
                }
                else
                {
                    var result = LibMpv.MpvObserveProperty(handle, 0, propertyName, MpvFormat.MpvFormatFlag);
                    if (result < 0) ThrowMpvError(result);
                    actionList = new();
                    actionList.Add(changedAction);
                    propertyBoolChangedActions.Add(propertyName, actionList);
                }
            }
        }
    }

    public void Command(params string[] args)
    {
        if (disposed || args == null || args.Length == 0)
            return;

        using var helper = new MarshalHelper();
        int result = LibMpv.MpvCommand(handle, (byte**)helper.CStringArrayForManagedUTF8StringArray(args));
        if (result < 0) ThrowMpvError(result);
    }

    public void SetPropertyFlag(string name, bool value)
    {
        if (disposed || handle == null || name == null)
            return;

        int result = 0;
        var longValue = new long[1] { value ? 1 : 0 };
        fixed (long* valuePtr = longValue)
        {
            result = LibMpv.MpvSetProperty(handle, name, MpvFormat.MpvFormatFlag, valuePtr);
        }
        if (result < 0) ThrowMpvError(result);
    }

    public bool? GetPropertyFlag (string name)
    {
        if (disposed || handle == null || name == null)
            return null;
        IntPtr valuePtr;
        int result = LibMpv.MpvGetProperty(handle, name, MpvFormat.MpvFormatFlag, &valuePtr);
        if (result < 0) return null;
        return valuePtr.ToInt32() != 0;
    }

    public void SetPropertyLong(string name, long newValue)
    {
        if (disposed || handle == null || name == null)
            return;

        int result = 0;
        var value = new long[1] { newValue };
        fixed (long* valuePtr = value)
        {
            result = LibMpv.MpvSetProperty(handle, name, MpvFormat.MpvFormatInt64, valuePtr);
        }
        if (result < 0) ThrowMpvError(result);
    }

    public long? GetPropertyLong(string name)
    {
        if (disposed) return null;
        int result;
        var value = new long[1] { 0 };
        fixed (long* valuePtr = value)
        {
            result = LibMpv.MpvGetProperty(handle, name, MpvFormat.MpvFormatInt64, valuePtr);
        }
        if (result < 0) return null;
        return value[0];
    }

    public void SetOptionString(string name, string value)
    {
        if (disposed || name == null)
            return;
        int result = LibMpv.MpvSetPropertyString(handle, name, value);
        if (result < 0) ThrowMpvError(result);
    }

    public void SetPropertyString(string name, string value)
    {
        if (disposed || name == null)
            return;
        int result = LibMpv.MpvSetPropertyString(handle, name, value);
        if (result < 0) ThrowMpvError(result);
    }

    public string? GetPropertyString(string name)
    {
        if (disposed || name == null)
            return null;
        var value = LibMpv.MpvGetPropertyString(handle, name);
        return value == null ? null : UTF8Marshaler.FromNative(Encoding.UTF8, value);
    }

    public void SetPropertyDouble(string name, double newValue)
    {
        if (disposed || name == null)
            return;

        int result;
        var value = new double[1] { newValue };
        fixed (double* valuePtr = value)
        {
            result = LibMpv.MpvSetProperty(handle, name, MpvFormat.MpvFormatDouble, valuePtr);
        }
        if (result < 0) ThrowMpvError(result);
    }

    public double? GetPropertyDouble(string name)
    {
        if (disposed || name == null)
            return null;

        int result;
        var value = new double[1] { 0 };
        fixed (double* valuePtr = value)
        {
            result = LibMpv.MpvGetProperty(handle, name, MpvFormat.MpvFormatDouble, valuePtr);
        }
        if (result < 0) return null;
        return value[0];
    }

    private void ThrowMpvError(int mpvError)
    {
        throw new MpvException(LibMpv.MpvErrorString(mpvError));
    }

    ~MpvContext()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                if (eventLoop != null)
                {
                    eventLoop.Dispose();
                }
            }
            if (renderContext != null)
                StopRendering();

            if (handle!=null)
            {
                LibMpv.MpvTerminateDestroy(handle);
                handle = null;
            }
            this.disposed = true;
        }
    }

    private bool disposed = false;
    private MpvHandle* handle = null;
    private MpvEventLoop eventLoop = null;
    private IMpvRendererConfiguration configuration = null;
    private MpvRenderContext* renderContext = null;
}
