using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IptvPlayer.Core;

public class M3UIptvListLoader
{
    static Regex attribute = new Regex("([a-zA-Z0-9_-]+)=(\"[^\"]+\"|[^\", ]+)");

    internal static IptvPlaylist Parse(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return Parse(reader);
    }

    internal static IptvPlaylist Parse(StreamReader reader)
    {
        var playlist = new IptvPlaylist();

        var line = reader.ReadLine();
        while (line != null)
        {
            line = line.Trim();
            if (line.Length > 0)
            {
                ParseLine(line, playlist);
            }
            line = reader.ReadLine();
        }
        return playlist;
    }

    internal static IptvPlaylist ParseFromFile(string filename)
    {
        using var stream = new System.IO.StreamReader(filename);
        return Parse(stream);
    }

    internal static async Task<IptvPlaylist> ParseFromUrl(string url)
    {
        using var client = new HttpClient();
        using var response = await client.GetAsync(url);
        using var stream = await response.Content.ReadAsStreamAsync();
        return Parse(stream);
    }

    internal static IptvPlaylist ParseFromString(string content)
    {
        using var stream = new MemoryStream();
        var bytes = Encoding.UTF8.GetBytes(content);
        stream.Write(bytes,0,bytes.Length);
        stream.Seek(0,SeekOrigin.Begin);
        return Parse(stream);
    }

    internal static void ParseLine(string line, IptvPlaylist playlist)
    {
        if (line.StartsWith("#EXTM3U"))
        {

            foreach (var (name, value) in ParseAttributes(line.Substring(7)))
            {
                switch (name)
                {
                    case "tvg-shift":
                        int tvgShift;
                        if (Int32.TryParse(value, out tvgShift))
                            playlist.TvgShift = tvgShift;
                        break;
                    case "url-tvg":
                        var items = value.Split(',').Select(it => it.Trim());
                        foreach (var item in items)
                            if (item.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                                playlist.TvgUrls.Add(item);
                        break;
                    case "catchup-correction":
                        int correction;
                        if (Int32.TryParse(value, out correction))
                            playlist.CatchupCorrection = correction;
                        break;
                }
            }
        }
        else
        {
            if (line.StartsWith("#EXTINF:"))
            {
                var channel = new IptvChannel();
                channel.ChannelName = line.Substring(line.LastIndexOf(',') + 1).Trim();
                line = line.Substring(line.IndexOf(':') + 1);
                foreach (var (name, value) in ParseAttributes(line))
                {
                    int intValue;
                    switch (name)
                    {
                        case "tvg-id":
                            channel.TvgId = value.Trim();
                            break;
                        case "tvg-name":
                            channel.TvgName = value.Trim();
                            break;
                        case "group-title":
                            var groups = value.Split(',').Select(it => it.Trim());
                            foreach (var group in groups)
                                channel.Groups.Add(group);
                            break;
                        case "tvg-chno":
                            if (Int32.TryParse(value, out intValue))
                                channel.TvgChno = intValue;
                            break;
                        case "tvg-logo":
                            channel.TvgLogo = value.Trim();
                            break;
                        case "radio":
                            channel.Radio = value.Trim().ToLower() == "true";
                            break;
                        case "tvg-shift":
                            if (Int32.TryParse(value, out intValue))
                                channel.TvgShift = intValue;
                            break;
                        case "catchup":
                            channel.Catchup = value.Trim();
                            break;
                        case "catchup-source":
                            channel.CatchupSource = value.Trim();
                            break;
                        case "catchup-days":
                        case "timeshift":
                        case "tvg-rec":
                            if (Int32.TryParse(value, out intValue))
                                channel.CatchupDays = intValue;
                            break;
                        case "catchup-correction":
                            if (Int32.TryParse(value, out intValue))
                                channel.CatchupCorrection = intValue;
                            break;
                    }
                }
                playlist.Channels.Add(channel);
            }
            else
            {
                var channel = playlist.Channels.LastOrDefault();
                if (channel == null)
                    return;
                else if (line.StartsWith("#EXTGRP:"))
                {
                    var groups = line.Substring(8).Split(',').Select(it => it.Trim());
                    foreach (var group in groups)
                        channel.Groups.Add(group);
                }
                else if (line.StartsWith("#EXTVLCOPT:"))
                {
                    var opt = line.Substring(11).Split(new char[] { '=' }, 1).Select(it => it.Trim(' ', '"')).ToArray();
                    if (opt.Length == 2 && opt[0].StartsWith("http-", StringComparison.OrdinalIgnoreCase))
                    {
                        channel.Headers[opt[0].ToLower().Replace("http-", "")] = opt[1].Trim();
                    }
                }
                else if (!line.StartsWith("#"))
                    channel.URL = line.Trim();
            }
        }
    }

    internal static IEnumerable<(string, string)> ParseAttributes(string line)
    {
        var attributes = attribute.Matches(line);
        foreach (Match attribute in attributes)
        {
            yield return (attribute.Groups[1].Value.Trim().ToLower(), attribute.Groups[2].Value.Trim(' ', '"'));
        }
    }

    public static async Task<IptvPlaylist> Load(string resource)
    {
        if (resource.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            return await ParseFromUrl(resource);
        return ParseFromFile(resource);
    }
}
