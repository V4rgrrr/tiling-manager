namespace TilingManager.Core.Models;

public class Zone
{
    public double X { get; set; } // Value from 0.0 to 1.0 (X position on screen)
    public double Y { get; set; } // Value from 0.0 to 1.0 (Y position on screen)
    public double Width { get; set; } // Width (percentage)
    public double Height { get; set; } // Height (percentage)
}