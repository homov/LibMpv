namespace LibMpv.Client;

public unsafe partial class MpvContext
{
    public event EventHandler Shutdown;

    public event EventHandler StartFile;

    public event EventHandler<MpvEndFileEventArgs> EndFile;

    public event EventHandler FileLoaded;

    public event EventHandler<MpvLogMessageEventArgs> LogMessage;

    public event EventHandler<MpvGetPropertyReplyEventArgs> GetPropertyReply;

    public event EventHandler<MpvSetPropertyReplyEventArgs> SetPropertyReply;

    private void HandleMpvEvent(MpvEvent mpvEvent)
    {
        switch (mpvEvent.EventId)
        {
            case MpvEventId.MpvEventShutdown:
                this.HandleShutdown();
                break;
            case MpvEventId.MpvEventStartFile:
                this.StartFile?.Invoke(this, EventArgs.Empty);
                break;
            case MpvEventId.MpvEventEndFile:
                this.HandleEndFile(mpvEvent);
                break;
            case MpvEventId.MpvEventFileLoaded:
                this.FileLoaded?.Invoke(this, EventArgs.Empty);
                break;
            case MpvEventId.MpvEventLogMessage:
                this.HandleLogMessage(mpvEvent);
                break;
            case MpvEventId.MpvEventGetPropertyReply:
                this.HandleGetPropertyReply(mpvEvent);
                break;
            case MpvEventId.MpvEventSetPropertyReply:
                this.HandleSetPropertyReply(mpvEvent);
                break;
            case MpvEventId.MpvEventPropertyChange:
                this.HandlePropertyChange(mpvEvent);
                break;
            default:
                return;
        }
    }

    private void HandlePropertyChange(MpvEvent mpvEvent)
    {
        var mpvProperty = MarshalHelper.PtrToStructure<MpvEventProperty>(mpvEvent.Data);
        if (mpvProperty == null)
            return;

        var property = MpvProperty.From(mpvProperty.Value);

        if (property.Format == MpvFormat.MpvFormatNone)
        {
            lock (propertyChangedActions)
            {
                List<Action> actionList;
                if (propertyChangedActions.TryGetValue(property.Name, out actionList))
                {
                    foreach (var action in actionList)
                        action.Invoke();
                }
            }
        }
        else if (property.Format == MpvFormat.MpvFormatFlag)
        {
            lock (propertyBoolChangedActions)
            {
                List<Action<bool>> actionList;
                if (propertyBoolChangedActions.TryGetValue(property.Name, out actionList))
                {
                    foreach (var action in actionList)
                        action.Invoke(property.BooleanValue.Value);
                }
            }
        }
    }

    private void HandleSetPropertyReply(MpvEvent mpvEvent)
    {
        if (this.SetPropertyReply != null)
        {
            this.SetPropertyReply.Invoke(this, new MpvSetPropertyReplyEventArgs(mpvEvent.ReplyUserdata, (MpvError)mpvEvent.Error));
        }
    }

    private void HandleGetPropertyReply(MpvEvent mpvEvent)
    {
        if (this.GetPropertyReply != null)
        {
            var propReply = MarshalHelper.PtrToStructure<MpvEventProperty>(mpvEvent.Data);
            if (propReply == null)
                return;

            this.GetPropertyReply.Invoke(this, new MpvGetPropertyReplyEventArgs(mpvEvent.ReplyUserdata, (MpvError)mpvEvent.Error, MpvProperty.From(propReply.Value)));
        }
    }

    private void HandleLogMessage(MpvEvent mpvEvent)
    {
        if (this.LogMessage != null)
        {
            var logMessage = MarshalHelper.PtrToStructure<MpvEventLogMessage>(mpvEvent.Data);
            if (logMessage == null)
                return;

            this.LogMessage.Invoke(
                this,
                new MpvLogMessageEventArgs(
                    logMessage.Value.LogLevel,
                    MarshalHelper.PtrToStringUTF8OrEmpty(logMessage.Value.Prefix),
                    MarshalHelper.PtrToStringUTF8OrEmpty(logMessage.Value.Level),
                    MarshalHelper.PtrToStringUTF8OrEmpty(logMessage.Value.Text)
                    ));
        }
    }

    private void HandleEndFile(MpvEvent mpvEvent)
    {
        if (this.EndFile != null)
        {
            var endFile = MarshalHelper.PtrToStructure<MpvEventEndFile>(mpvEvent.Data);
            if (endFile == null)
                return;
            this.EndFile.Invoke(this, new MpvEndFileEventArgs(endFile.Value));
        }
    }

    private void HandleShutdown()
    {
        this.eventLoop.Stop();
        this.Shutdown?.Invoke(this, EventArgs.Empty);
    }

    Dictionary<string, List<Action>> propertyChangedActions = new Dictionary<string, List<Action>>();
    Dictionary<string, List<Action<bool>>> propertyBoolChangedActions = new Dictionary<string, List<Action<bool>>>();
}


public class MpvEndFileEventArgs : EventArgs
{
    public MpvEndFileEventArgs(MpvEventEndFile eventData)
    {
        EventData = eventData;
    }

    public MpvEventEndFile EventData { get; }
}

public class MpvLogMessageEventArgs : EventArgs
{
    public MpvLogMessageEventArgs(MpvLogLevel logLevel, string prefix, string level, string text)
    {
        LogLevel = logLevel;
        Prefix = prefix;
        Level = level;
        Text = text;
    }

    public MpvLogLevel LogLevel { get; }
    public string Prefix { get; }
    public string Level { get; }
    public string Text { get; }
}

public class MpvGetPropertyReplyEventArgs : EventArgs
{
    public MpvGetPropertyReplyEventArgs(ulong replyUserData, MpvError error, MpvProperty eventData)
    {
        ReplyUserData = replyUserData;
        Error = error;
        EventData = eventData;
    }

    public ulong ReplyUserData { get; }
    public MpvError Error { get; }
    public MpvProperty EventData { get; }
}

public class MpvSetPropertyReplyEventArgs : EventArgs
{
    public MpvSetPropertyReplyEventArgs(ulong replyUserData, MpvError error)
    {
        ReplyUserData = replyUserData;
        Error = error;
    }

    public ulong ReplyUserData { get; }
    public MpvError Error { get; }
}