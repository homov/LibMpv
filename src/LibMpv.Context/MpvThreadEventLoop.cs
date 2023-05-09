using static LibMpv.Client.libmpv;
namespace LibMpv.Client;

public unsafe class MpvThreadEventLoop : IEventLoop, IDisposable
{
    public MpvThreadEventLoop(mpv_handle* context, Action<mpv_event> eventHandler)
    {
        this.context = context;
        handleEvent = eventHandler;
    }


    public void Stop()
    {
        if (isEventLoopRunning)
        {
            isEventLoopRunning = false;
            mpv_wakeup(context);
            evenLoopThread.Join();
        }
    }

    public void Start()
    {
        evenLoopThread = new Thread(ProcessEvents);
        isEventLoopRunning = true;
        evenLoopThread.Start();
    }

    private void ProcessEvents()
    {
        while (isEventLoopRunning)
        {
            var eventPtr = mpv_wait_event(context, -1);
            if (eventPtr != null)
            {
                var @event = MarshalHelper.PtrToStructure<mpv_event>((nint)eventPtr);
                if (@event.event_id != mpv_event_id.MPV_EVENT_NONE)
                {
                    handleEvent(@event);
                }
            }
        }
    }

    public void Dispose()
    {
        if (!disposed)
        {
            Stop();
            disposed = true;
        }
    }

    protected volatile bool isEventLoopRunning;
    protected Thread? evenLoopThread;
    private readonly mpv_handle* context;
    private readonly Action<mpv_event> handleEvent;
    private bool disposed = false;

}
