using TilingManager.Core.Models;

namespace TilingManager.Engine;

public class ZoneCalculator
{
    public (int X, int Y, int Width, int Height)  CalculateAbsoluteCoordinates(Zone zone)
    {
        int screenWidth = NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN);
        int screenHeight = NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN);
        
        int x = (int)Math.Round(screenWidth * zone.X);
        int y = (int)Math.Round(screenHeight * zone.Y);
        int width = (int)Math.Round(screenWidth * zone.Width);
        int height = (int)Math.Round(screenHeight * zone.Height);
        
        return (x, y, width, height);
    }
    
    // TEST ONLY!!!
    public (int X, int Y, int Width, int Height) CalculateAbsoluteCoordinates(Zone zone, int screenWidth, int screenHeight)
    {
        int x = (int)Math.Round(screenWidth * zone.X);
        int y = (int)Math.Round(screenHeight * zone.Y);
        int width = (int)Math.Round(screenWidth * zone.Width);
        int height = (int)Math.Round(screenHeight * zone.Height);
        
        return (x, y, width, height);
    }
}