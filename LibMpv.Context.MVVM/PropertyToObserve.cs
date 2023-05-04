using LibMpv.Client;

namespace LibMpv.Context.MVVM;

public class PropertyToObserve
{
    public string MvvmName { get; set; }
    public string LibMpvName { get; set; }
    public mpv_format LibMpvFormat { get; set; }
}
