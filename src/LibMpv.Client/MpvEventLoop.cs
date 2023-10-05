namespace LibMpv.Client;

internal unsafe class MpvEventLoop : IDisposable
{
    private bool disposed = false;
    private bool isRunning = false;
    private MpvHandle* handle = null;
    private Action<MpvEvent> eventHandler = null;
    private Task eventLoopTask = null;

    public MpvEventLoop(MpvHandle* handle, Action<MpvEvent> eventHandler)
    {
        this.handle = handle;
        this.eventHandler = eventHandler;
    }

    public void Start()
    {
        if(!this.disposed)
        {
            this.DisposeEventLoopTask();
            this.isRunning = true;
            this.eventLoopTask = Task.Factory.StartNew( new Action(EventLoop), TaskCreationOptions.LongRunning  | TaskCreationOptions.DenyChildAttach);
        }
    }

    public void Stop()
    {
        this.isRunning = false;
        if (this.disposed)
            return;
        if (handle != null)
            LibMpv.MpvWakeup(handle);
        this.DisposeEventLoopTask();
    }

    private void EventLoop()
    {
        try
        {
            while (this.isRunning)
            {
                MpvEvent* mpvEvent  = LibMpv.MpvWaitEvent(handle, -1.0);
                if (mpvEvent != null)
                {
                    if (mpvEvent->EventId != MpvEventId.MpvEventNone)
                    {
                        if (eventHandler != null)
                        {
                            var @event = MarshalHelper.PtrToStructure<MpvEvent>((IntPtr)mpvEvent);
                            if (@event != null)
                            {
                                try
                                {
                                    eventHandler?.Invoke(@event.Value);
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
        }
        this.isRunning = false;
    }

    private void DisposeEventLoopTask()
    {
        isRunning = false;
        try
        {
            if (this.eventLoopTask!=null)
            {
                this.eventLoopTask.Dispose();
                this.eventLoopTask = null;
            }
        }
        catch
        {

        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!this.disposed)
            {
                if (isRunning)
                    Stop();
            }
            this.disposed = true;
        }
    }
}

