namespace IptvPlayer.Core.Model;

public class Settings
{
    public const string AllChannels = "All Channels";

    public string PlayList { get; set; } = "https://iptv-org.github.io/iptv/index.category.m3u";

    public string LastGroup { get; set; } = AllChannels;

}