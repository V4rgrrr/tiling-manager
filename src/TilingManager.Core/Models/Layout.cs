namespace TilingManager.Core.Models;

public class Layout
{
    public string Name { get; set; } = string.Empty;
    public List<Zone> Zones { get; set; } = new List<Zone>();
}