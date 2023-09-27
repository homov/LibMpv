using System.Runtime.InteropServices;

namespace LibMpv.Client;

public class MpvProperty
{
    public const string Volume = "volume";
    public const string Mute = "mute";
    public const string Path = "path";
    public const string Seekable = "seekable";
    public const string Pause = "pause";
    public const string Duration = "duration";
    public const string TimePos = "time-pos";
    public const string CacheBufferingState = "cache-buffering-state";
    public const string PausedForCache = "paused-for-cache";
    public const string PercentPos = "percent-pos";
    public const string TimeRemaining = "time-remaining";

    public const string UserAgent = "user-agent";
    public const string Referrer = "referrer";
    public const string HttpHeaderFields = "http-header-fields";

    public MpvProperty(string name, MpvFormat format, string? stringValue = null, long? longValue = null, double? doubleValue = null)
    {
        Name = name;
        Format = format;
        StringValue = stringValue;
        LongValue = longValue;
        DoubleValue = doubleValue;
    }


    public string Name { get; }
    public MpvFormat Format { get; }
    public string? StringValue { get; }
    public long? LongValue { get; }
    public double? DoubleValue { get; }

    public static unsafe MpvProperty From(MpvEventProperty eventProperty)
    {
        var name = MarshalHelper.PtrToStringUTF8OrEmpty(eventProperty.Name);
        var format = eventProperty.Format;
        string? stringValue = null;
        long? longValue = null;
        double? doubleValue = null;
        if (format == MpvFormat.MpvFormatString && eventProperty.Data != null)
            stringValue = MarshalHelper.PtrToStringUTF8OrNull((IntPtr)eventProperty.Data);
        else if (format == MpvFormat.MpvFormatInt64 && eventProperty.Data != null)
            longValue = Marshal.ReadInt64((IntPtr)eventProperty.Data);
        else if (format == MpvFormat.MpvFormatDouble && eventProperty.Data != null)
        {
            var bytes = new byte[8];
            Marshal.Copy((IntPtr)eventProperty.Data, bytes, 0, 8);
            doubleValue = BitConverter.ToDouble(bytes, 0);
        }
        return new MpvProperty(name, format, stringValue, longValue, doubleValue);
    }
}
