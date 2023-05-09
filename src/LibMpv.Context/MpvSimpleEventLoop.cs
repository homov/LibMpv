using static LibMpv.Client.libmpv;

namespace LibMpv.Client;

public unsafe class MpvSimpleEventLoop : IEventLoop, IDisposable
{
    public MpvSimpleEventLoop(mpv_handle* context, Action<mpv_event> eventHandler)
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

            if (Task.CurrentId == evenLoopTask.Id)
            {
                return;
            }
            evenLoopTask.Wait();
        }
    }

    public void Start()
    {
        evenLoopTask?.Dispose();
        evenLoopTask = new Task(ProcessEvents);
        isEventLoopRunning = true;
        evenLoopTask.Start();
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
            evenLoopTask?.Dispose();
            disposed = true;
        }
    }

    protected volatile bool isEventLoopRunning;
    protected Task? evenLoopTask;
    private readonly mpv_handle* context;
    private readonly Action<mpv_event> handleEvent;
    private bool disposed = false;
}
