using static LibMpv.Client.libmpv;
namespace LibMpv.Client;

public unsafe class MpvWeakEventLoop: IEventLoop
{
    
    public MpvWeakEventLoop(mpv_handle* context, Action<mpv_event> eventHandler)
    {
        this.context = context;
        handleEvent = eventHandler;
        wakeupCallback = new mpv_set_wakeup_callback_cb( WakeupCallback );
    }

    private void WakeupHandleEvent()
    {
        lock (wakeUpLock)
        {
            while (isEventLoopRunning)
            {
                var eventPtr = mpv_wait_event(context, 0);
                if (eventPtr != null)
                {
                    var @event = MarshalHelper.PtrToStructure<mpv_event>((nint)eventPtr);
                    if (@event.event_id != mpv_event_id.MPV_EVENT_NONE)
                    {
                        handleEvent(@event);
                    }
                    else
                        break;
                }
                else
                {
                    break;
                }
            }
        }
    }


    private void WakeupCallback(void* _)
    {
        Console.WriteLine("WakeupCallback");
        if (isEventLoopRunning)
            Task.Run( async () => WakeupHandleEvent());
    }

    public void Stop()
    {
        isEventLoopRunning = false;
    }

    public void Start()
    {
        isEventLoopRunning = true;
        mpv_set_wakeup_callback(context, wakeupCallback, null);
    }

    protected object wakeUpLock = new object();
    protected volatile bool isEventLoopRunning;
    private readonly mpv_handle* context;
    private readonly Action<mpv_event> handleEvent;
    private mpv_set_wakeup_callback_cb wakeupCallback;
}

