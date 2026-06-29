using TilingManager.Core.Models;
using System.Diagnostics;

namespace TilingManager.Engine.Tests;

public class TilingEngineTests
{
    [Fact]
    public void Should_Tile_Notepad_To_Right_Half_Of_Screen()
    {
        // Arrange
        var windowManager = new WindowManager();
        var zoneCalculator = new ZoneCalculator();
        var engine = new TilingEngine(windowManager, zoneCalculator);
        
        var executablePath = @"C:\Windows\System32\notepad.exe";

        var rightHalfZone = new Zone { X = 0.5, Y = 0.0, Width = 0.5, Height = 1.0 };
        
        // Act
        var windowHandle = windowManager.LaunchAppAndGetHandle(executablePath);
        Assert.NotEqual(IntPtr.Zero, windowHandle);
        
        bool tileSuccess = engine.TileWindow(windowHandle, rightHalfZone);
        
        // Assert
        Assert.True(tileSuccess);
        
        Thread.Sleep(2000);
        
        // Cleanup
        if (windowHandle != IntPtr.Zero)
        {
            Process processToKill = Process.GetProcessById(windowManager.GetProcessIdFromWindowHandle(windowHandle));
            processToKill.Kill();
            processToKill.WaitForExit();
        }
    }
}