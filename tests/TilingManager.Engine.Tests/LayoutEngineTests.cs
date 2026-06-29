using TilingManager.Core.Models;
using System.Diagnostics;

namespace TilingManager.Engine.Tests;

public class LayoutEngineTests
{
    [Fact]
    public void Should_Apply_TwoColumns_Layout_To_Multiple_Notepad_Windows()
    {
        // Arrange
        var windowManager = new WindowManager();
        var zoneCalculator = new ZoneCalculator();
        var engine = new TilingEngine(windowManager, zoneCalculator);
        
        var executablePath = @"C:\Windows\System32\notepad.exe";
        var layout = PredefinedLayouts.TwoColumns;
        
        // Act
        IntPtr handle1 = windowManager.LaunchAppAndGetHandle(executablePath);
        IntPtr handle2 = windowManager.LaunchAppAndGetHandle(executablePath);
        
        var windowsList = new List<IntPtr> { handle1, handle2 };
        
        bool result = engine.ApplyLayout(layout, windowsList);
        
        // Assert
        Assert.True(result);
        
        Thread.Sleep(2000);
        
        // Cleanup
        foreach (var handle in windowsList)
        {
            if (handle != IntPtr.Zero)
            {
                try
                {
                    int pid = windowManager.GetProcessIdFromWindowHandle(handle);
                    using Process p = Process.GetProcessById(pid);
            
                    if (!p.HasExited)
                    {
                        p.Kill();
                        p.WaitForExit(1000);
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }
    }
}