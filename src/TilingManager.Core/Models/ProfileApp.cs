namespace TilingManager.Core.Models;

public class ProfileApp
{
    public string ExecutablePath { get; set; } = string.Empty;
    public string Arguments { get; set; } = string.Empty;
    public int TargetZoneIndex { get; set; } // Index of the zone from selected layout
}