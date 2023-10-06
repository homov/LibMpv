using System;
using System.Collections.Generic;

namespace IptvPlayer.Core;

public class IptvPlaylist
{
    public IList<string> TvgUrls { get; set; } = new List<string>();

    public int? TvgShift { get; set; } = null;

    public int? CatchupCorrection { get; set; } = null;

    public IList<IptvChannel> Channels { get; set; } = new List<IptvChannel>();

    public void SaveTo(System.IO.StreamWriter streamWriter)
    {
        //Header
        streamWriter.Write("#EXTM3U");
        if (TvgUrls.Count > 0)
            streamWriter.Write($" url-tvg=\"{String.Join(",", TvgUrls)}\"");
        if (TvgShift != null)
            streamWriter.Write($" tvg-shift=\"{TvgShift}\"");
        if (CatchupCorrection != null)
            streamWriter.Write($" catchup-correction=\"{CatchupCorrection}\"");
        streamWriter.WriteLine();

        //Channels
        foreach (var channel in Channels)
        {
            streamWriter.Write("#EXTINF:-1");
            if (channel.Groups.Count > 0)
                streamWriter.Write($" group-title=\"{String.Join(",", channel.Groups)}\"");
            if (channel.TvgId != null)
                streamWriter.Write($" tvg-id=\"{channel.TvgId}\"");
            if (channel.TvgName != null)
                streamWriter.Write($" tvg-name=\"{channel.TvgName}\"");
            if (channel.TvgChno != null)
                streamWriter.Write($" tvg-chno=\"{channel.TvgChno}\"");
            if (channel.TvgLogo != null)
                streamWriter.Write($" tvg-logo=\"{channel.TvgLogo}\"");
            if (channel.Radio == true)
                streamWriter.Write(" radio=\"true\"");
            if (channel.TvgShift != null)
                streamWriter.Write($" tvg-shift=\"{channel.TvgShift}\"");
            if (channel.Catchup != null)
            {
                streamWriter.Write($" catchup=\"{channel.Catchup}\"");
                if (channel.CatchupSource != null)
                    streamWriter.Write($" catchup-source=\"{channel.CatchupSource}\"");
                if (channel.CatchupDays != null)
                    streamWriter.Write($" catchup-days=\"{channel.CatchupDays}\"");
                if (channel.CatchupCorrection != null)
                    streamWriter.Write($" catchup-correction=\"{channel.CatchupCorrection}\"");
            }

            streamWriter.WriteLine($",{channel.ChannelName}");
            if (channel.Headers.Count > 0)
            {
                foreach (var header in channel.Headers)
                {
                    streamWriter.WriteLine($"#EXTVLCOPT:http-{header.Key}={header.Value}");
                }
            }
            streamWriter.WriteLine(channel.URL);
        }
    }

    public void SaveTo(string path)
    {
        using var streamWriter = System.IO.File.CreateText(path);
        SaveTo(streamWriter);
    }
}
