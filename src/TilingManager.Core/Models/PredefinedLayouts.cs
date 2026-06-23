namespace TilingManager.Core.Models;

public class PredefinedLayouts
{
    public static Layout TwoColumns => new Layout()
    {
        Name = "Two Columns",
        Zones = new List<Zone>()
        {
            new Zone() { X = 0.0, Y = 0.0, Width = 0.5, Height = 1.0 },
            new Zone() { X = 0.5, Y = 0.0, Width = 0.5, Height = 1.0 }
        }
    };

    public static Layout MasterStackLeft => new Layout()
    {
        Name = "Master & Stack (Left)",
        Zones = new List<Zone>()
        {
            new Zone() { X = 0.0, Y = 0.0, Width = 0.5, Height = 1.0 }, // Left (Master)
            new Zone() { X = 0.5, Y = 0.0, Width = 0.5, Height = 0.5 }, // Top Right (Stack)
            new Zone() { X = 0.5, Y = 0.5, Width = 0.5, Height = 0.5 } // Bottom Right (Stack)
        }
    };
    
    public static Layout MasterStackRight => new Layout()
    {
        Name = "Master & Stack (Right)",
        Zones = new List<Zone>()
        {
            new Zone() { X = 0.0, Y = 0.0, Width = 0.5, Height = 0.5 }, // Left Top (Stack)
            new Zone() { X = 0.5, Y = 0.5, Width = 0.5, Height = 0.5 }, // Bottom Left (Stack)
            new Zone() { X = 0.5, Y = 0.0, Width = 0.5, Height = 1.0 } // Right (Master)
        }
    };
}