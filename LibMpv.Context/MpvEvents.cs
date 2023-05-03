using System;

namespace LibMpv.Client;

public class MpvPropertyEventArgs : EventArgs
{
    public MpvPropertyEventArgs(mpv_format format, string name, object? value, ulong replyData, int errorCode = 0)
    {
        Format = format;
        Name = name;
        Value = value;
        ReplyData = replyData;
        ErrorCode = errorCode;
    }

    public mpv_format Format { get; }
    public string Name { get; }
    public object? Value { get; }
    public ulong ReplyData { get; }
    public int ErrorCode { get; }
}

public class MpvReplyEventArgs: EventArgs
{
    public MpvReplyEventArgs(ulong replyData, int errorCode)
    {
        ReplyData = replyData;
        ErrorCode = errorCode;
    }

    public ulong ReplyData { get; }
    public int ErrorCode { get; }
}

public class MpvLogMessageEventArgs : EventArgs
{
    public MpvLogMessageEventArgs(string prefix, string level, string text, mpv_log_level logLevel)
    {
        Prefix = prefix;
        Level = level;
        Text = text;
        LogLevel = logLevel;
    }

    public string Prefix { get; }
    public string Level { get; }
    public string Text { get; }
    public mpv_log_level LogLevel { get; }
}

public class MpvEndFileEventArgs : EventArgs
{
    public MpvEndFileEventArgs(mpv_end_file_reason reason, int error, long playListEntryId)
    {
        Reason = reason;
        Error = error;
        PlayListEntryId = playListEntryId;
    }

    public mpv_end_file_reason Reason { get; }
    public int Error { get; }
    public long PlayListEntryId { get; }
}

public class MpvStartFileEventArgs : EventArgs
{
    public MpvStartFileEventArgs(long playListEntryId)
    {
        PlayListEntryId = playListEntryId;
    }
    public long PlayListEntryId { get; }
}
