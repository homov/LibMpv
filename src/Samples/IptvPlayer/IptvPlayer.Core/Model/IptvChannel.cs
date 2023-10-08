using System;
using System.Collections.Generic;
using System.Linq;

namespace IptvPlayer.Core;

public class IptvChannel
{
    public string? TvgId { get; set; } = null;
    
    public string? TvgName { get; set; } = null;
    
    public IList<string> Groups { get; set; } = new List<string>();
    
    public IDictionary<string,string> Headers { get; set; } = new Dictionary<string,string>();

    public int? TvgChno { get; set; } = null;

    public string? TvgLogo { get; set; } = null;
    
    public bool Radio { get; set; } = false;

    public int? TvgShift { get; set; } = null;

    public string? Catchup { get; set; } = null;
    
    public string? CatchupSource { get; set; } = null;
    
    public int? CatchupDays { get; set; } = null;
    
    public int? CatchupCorrection { get; set; } = null;

    public string ChannelName { get; set; } = String.Empty;

    public string URL { get; set; } = String.Empty;

    public string? GroupTitle => this.Groups.FirstOrDefault();

    public string? Country { get; set; }
}