using TilingManager.Core.Models;
using System.Diagnostics;

namespace TilingManager.Engine.Tests;

public class LayoutEngineTests
{
    // [Fact]
    // public void Should_Apply_TwoColumns_Layout_To_Multiple_Notepad_Windows()
    // {
    //     // Arrange
    //     var windowManager = new WindowManager();
    //     var zoneCalculator = new ZoneCalculator();
    //     var engine = new TilingEngine(windowManager, zoneCalculator);
    //     
    //     var executablePath = @"C:\Windows\System32\notepad.exe";
    //     var layout = PredefinedLayouts.TwoColumns;
    //     
    //     // Act
    //     IntPtr handle1 = windowManager.LaunchAppAndGetHandle(executablePath);
    //     IntPtr handle2 = windowManager.LaunchAppAndGetHandle(executablePath);
    //     
    //     var windowsList = new List<IntPtr> { handle1, handle2 };
    //     
    //     bool result = engine.ApplyLayout(layout, windowsList);
    //     
    //     // Assert
    //     Assert.True(result);
    //     
    //     Thread.Sleep(2000);
    //     
    //     // Cleanup
    //     foreach (var handle in windowsList)
    //     {
    //         if (handle != IntPtr.Zero)
    //         {
    //             try
    //             {
    //                 int pid = windowManager.GetProcessIdFromWindowHandle(handle);
    //                 using Process p = Process.GetProcessById(pid);
    //         
    //                 if (!p.HasExited)
    //                 {
    //                     p.Kill();
    //                     p.WaitForExit(1000);
    //                 }
    //             }
    //             catch (Exception)
    //             {
    //                 
    //             }
    //         }
    //     }
    // }
    
    [Fact(Timeout = 6000)]
    public async Task Should_Apply_MasterStackRight_Layout_To_Multiple_Notepad_Windows()
    {
        // Arrange
        var windowManager = new WindowManager();
        var zoneCalculator = new ZoneCalculator();
        var engine = new TilingEngine(windowManager, zoneCalculator);
    
        var executablePath = @"C:\Windows\System32\notepad.exe";
        var layout = PredefinedLayouts.MasterStackRight;
    
        // Act
        IntPtr handle1 = windowManager.LaunchAppAndGetHandle(executablePath);
        await Task.Delay(500);
        IntPtr handle2 = windowManager.LaunchAppAndGetHandle(executablePath);
        await Task.Delay(500);
        IntPtr handle3 = windowManager.LaunchAppAndGetHandle(executablePath);
        await Task.Delay(500);
    
        Assert.NotEqual(IntPtr.Zero, handle1);
        Assert.NotEqual(IntPtr.Zero, handle2);
        Assert.NotEqual(IntPtr.Zero, handle3);
    
        var windowsList = new List<IntPtr> { handle1, handle2, handle3 };
    
        bool result = engine.ApplyLayout(layout, windowsList);
    
        // Assert
        Assert.True(result);
    
        await Task.Delay(2000);
    
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
                        await p.WaitForExitAsync();
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}