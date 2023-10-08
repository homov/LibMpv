using System.Runtime.InteropServices;

namespace LibMpv.Client;

public class MpvProperty
{
    public const string UserAgent = "user-agent";
    public const string Referrer = "referrer";
    public const string HttpHeaderFields = "http-header-fields";

    public MpvProperty(string name, MpvFormat format, string? stringValue = null, long? longValue = null, double? doubleValue = null, bool? boolValue = null)
    {
        Name = name;
        Format = format;
        StringValue = stringValue;
        LongValue = longValue;
        DoubleValue = doubleValue;
        BooleanValue = boolValue;
    }


    public string Name { get; }
    public MpvFormat Format { get; }
    public string? StringValue { get; }
    public long? LongValue { get; }
    public double? DoubleValue { get; }

    public bool? BooleanValue { get; }

    public static unsafe MpvProperty From(MpvEventProperty eventProperty)
    {
        var name = MarshalHelper.PtrToStringUTF8OrEmpty(eventProperty.Name);
        var format = eventProperty.Format;
        string? stringValue = null;
        long? longValue = null;
        double? doubleValue = null;
        bool? boolValue = null;
        if (format == MpvFormat.MpvFormatString && eventProperty.Data != null)
            stringValue = MarshalHelper.PtrToStringUTF8OrNull((IntPtr)eventProperty.Data);
        else if (format == MpvFormat.MpvFormatInt64 && eventProperty.Data != null)
            longValue = Marshal.ReadInt64((IntPtr)eventProperty.Data);
        else if (format == MpvFormat.MpvFormatFlag && eventProperty.Data != null)
            boolValue = Marshal.ReadInt32((IntPtr)eventProperty.Data) == 1 ? true : false;
        else if (format == MpvFormat.MpvFormatDouble && eventProperty.Data != null)
        {
            var bytes = new byte[8];
            Marshal.Copy((IntPtr)eventProperty.Data, bytes, 0, 8);
            doubleValue = BitConverter.ToDouble(bytes, 0);
        }
        return new MpvProperty(name, format, stringValue, longValue, doubleValue, boolValue);
    }
}
