using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LibMpv.Client;

public unsafe partial class MpvContext
{
    protected delegate void MpvEventHandler(mpv_event @event);

    public event EventHandler? Shutdown;
    public event EventHandler<MpvStartFileEventArgs>? StartFile;
    public event EventHandler<MpvEndFileEventArgs>? EndFile;
    public event EventHandler? FileLoaded;
    public event EventHandler? Idle;
    public event EventHandler? Tick;
    public event EventHandler? VideoReconfig;
    public event EventHandler? AudioReconfig;
    public event EventHandler? Seek;
    public event EventHandler? PlaybackRestart;
    public event EventHandler? QueueOverflow;
    public event EventHandler<MpvPropertyEventArgs>? PropertyChanged;
    public event EventHandler<MpvReplyEventArgs>? AsyncCommandReply;
    public event EventHandler<MpvPropertyEventArgs>? AsyncGetPropertyReply;
    public event EventHandler<MpvReplyEventArgs>? AsyncSetPropertyReply;
    public event EventHandler<MpvLogMessageEventArgs>? LogMessage;

    private void InitEventHandlers()
    {
        eventHandlers = new Dictionary<mpv_event_id, MpvEventHandler>()
        {
            { mpv_event_id.MPV_EVENT_NONE, TraceHandler },
            { mpv_event_id.MPV_EVENT_SHUTDOWN, ShutdownHandler },
            { mpv_event_id.MPV_EVENT_LOG_MESSAGE, LogMessageHandler },
            { mpv_event_id.MPV_EVENT_GET_PROPERTY_REPLY, AsyncGetPropertyHandler },
            { mpv_event_id.MPV_EVENT_SET_PROPERTY_REPLY,  AsyncSetPropertyHandler },
            { mpv_event_id.MPV_EVENT_COMMAND_REPLY, AsyncCommandReplyHandler },
            { mpv_event_id.MPV_EVENT_START_FILE, StartFileHandler },
            { mpv_event_id.MPV_EVENT_END_FILE, EndFileHandler },
            { mpv_event_id.MPV_EVENT_FILE_LOADED, FileLoadedHandler },
            { mpv_event_id.MPV_EVENT_IDLE, IdleHandler },
            { mpv_event_id.MPV_EVENT_TICK, TickHandler },
            { mpv_event_id.MPV_EVENT_CLIENT_MESSAGE, TraceHandler },
            { mpv_event_id.MPV_EVENT_VIDEO_RECONFIG, VideoReconfigHandler },
            { mpv_event_id.MPV_EVENT_AUDIO_RECONFIG, AudioReconfigHandler },
            { mpv_event_id.MPV_EVENT_SEEK, SeekHandler },
            { mpv_event_id.MPV_EVENT_PLAYBACK_RESTART, PlaybackRestartHandler },
            { mpv_event_id.MPV_EVENT_PROPERTY_CHANGE, PropertyChangedHandler },
            { mpv_event_id.MPV_EVENT_QUEUE_OVERFLOW, QueueOverflowHandler },
            { mpv_event_id.MPV_EVENT_HOOK, TraceHandler },
        };
    }

    private void LogMessageHandler(mpv_event @event)
    {
        if (@event.data != null && LogMessage !=null)
            LogMessage?.Invoke(this, ToLogMessageEventArgs(@event));
    }

    private void AsyncSetPropertyHandler(mpv_event @event)
    {
        if (@event.data != null && AsyncSetPropertyReply!=null)
            AsyncSetPropertyReply?.Invoke(this, new MpvReplyEventArgs(@event.reply_userdata, @event.error));
    }

    private void AsyncGetPropertyHandler(mpv_event @event)
    {
        if (@event.data != null && AsyncGetPropertyReply!=null)
            AsyncGetPropertyReply?.Invoke(this, ToPropertyChangedEventArgs(@event));
    }

    private void AsyncCommandReplyHandler(mpv_event @event)
    {
        if (@event.data != null)
            AsyncCommandReply?.Invoke(this, new MpvReplyEventArgs(@event.reply_userdata, @event.error));
    }

    private void PropertyChangedHandler(mpv_event @event)
    {
        if (@event.data != null && PropertyChanged!=null)
            PropertyChanged?.Invoke(this, ToPropertyChangedEventArgs(@event));
    }

    private void QueueOverflowHandler(mpv_event @event)
    {
        QueueOverflow?.Invoke(this, EventArgs.Empty);
    }

    private void PlaybackRestartHandler(mpv_event @event)
    {
        PlaybackRestart?.Invoke(this, EventArgs.Empty);
    }

    private void SeekHandler(mpv_event @event)
    {
        Seek?.Invoke(this, EventArgs.Empty);
    }

    private void AudioReconfigHandler(mpv_event @event)
    {
        AudioReconfig?.Invoke(this, EventArgs.Empty);
    }

    private void VideoReconfigHandler(mpv_event @event)
    {
        VideoReconfig?.Invoke(this, EventArgs.Empty);
    }

    private void TickHandler(mpv_event @event)
    {
        Tick?.Invoke(this, EventArgs.Empty);
    }

    private void IdleHandler(mpv_event @event)
    {
        Idle?.Invoke(this, EventArgs.Empty);
    }

    private void FileLoadedHandler(mpv_event mpvEvent)
    {
        FileLoaded?.Invoke(this, EventArgs.Empty);
    }

    private void EndFileHandler(mpv_event @event)
    {
        if (@event.data != null && EndFile!=null)
        {
            mpv_event_end_file endFile = MarshalHelper.PtrToStructure<mpv_event_end_file>((nint)@event.data);
            EndFile?.Invoke(this, new MpvEndFileEventArgs(endFile.reason, endFile.error, endFile.playlist_entry_id));
        }
    }

    private void StartFileHandler(mpv_event @event)
    {
        if (@event.data != null && StartFile!=null)
        {
            mpv_event_start_file startFile = MarshalHelper.PtrToStructure<mpv_event_start_file>((nint)@event.data);
            StartFile?.Invoke(this, new MpvStartFileEventArgs(startFile.playlist_entry_id));
        }
    }

    private void ShutdownHandler(mpv_event @event)
    {
        Shutdown?.Invoke(this, EventArgs.Empty);
    }

    private void TraceHandler(mpv_event @event)
    {
        Debug.WriteLine($"Unhandled MPV Event: {Enum.GetName(typeof(mpv_event_id), @event.event_id)}");
    }


    private void HandleEvent(mpv_event @event)
    {
        MpvEventHandler eventHandler;
        if (eventHandlers.TryGetValue(@event.event_id, out eventHandler))
            eventHandler.Invoke(@event);
    }

    private MpvLogMessageEventArgs ToLogMessageEventArgs(mpv_event @event)
    {
        mpv_event_log_message logMessage = MarshalHelper.PtrToStructure<mpv_event_log_message>((nint)@event.data);
        return new MpvLogMessageEventArgs(
            MarshalHelper.PtrToStringUTF8OrEmpty((nint)logMessage.prefix),
            MarshalHelper.PtrToStringUTF8OrEmpty((nint)logMessage.level),
            MarshalHelper.PtrToStringUTF8OrEmpty((nint)logMessage.text),
            logMessage.log_level
        );
    }


    private MpvPropertyEventArgs ToPropertyChangedEventArgs(mpv_event @event)
    {
        mpv_event_property property = MarshalHelper.PtrToStructure<mpv_event_property>((nint)@event.data);

        object? value = null;

        if (property.format == mpv_format.MPV_FORMAT_STRING)
        {
            value = MarshalHelper.PtrToStringUTF8OrNull((nint)property.data);
        }
        else if (property.format == mpv_format.MPV_FORMAT_INT64)
        {
            value = Marshal.ReadInt64((nint)property.data);
        }
        else if (property.format == mpv_format.MPV_FORMAT_FLAG)
        {
            int flag;
            flag = Marshal.ReadInt32((nint)property.data);
            value = flag == 1 ? true : false;
        }
        else if (property.format == mpv_format.MPV_FORMAT_DOUBLE)
        {
            var doubleBytes = new byte[sizeof(double)];
            Marshal.Copy((nint)property.data, doubleBytes, 0, sizeof(double));
            value = BitConverter.ToDouble(doubleBytes, 0);
        }
        var name = MarshalHelper.PtrToStringUTF8OrEmpty((nint)property.name);
        return new MpvPropertyEventArgs(property.format, name, value, @event.reply_userdata, @event.error);
    }

    private Dictionary<mpv_event_id, MpvEventHandler>? eventHandlers;
}
