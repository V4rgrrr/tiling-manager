using System.Diagnostics;

namespace TilingManager.Engine.Tests;

public class WindowManagerTests
{
    [Fact]
    public void Should_Launch_Notepad_App_And_Return_Window_Handle()
    {
        // Arrange
        var windowManager = new WindowManager();
        var executablePath = @"C:\Windows\System32\notepad.exe";
        
        // Act
        var windowHandle = windowManager.LaunchAppAndGetHandle(executablePath);
        
        // Assert
        Assert.NotEqual(IntPtr.Zero, windowHandle);
        
        // Cleanup
        if (windowHandle != IntPtr.Zero)
        {
            int processId = windowManager.GetProcessIdFromWindowHandle(windowHandle);
            Process processToKill = Process.GetProcessById((int)processId);
            processToKill.Kill();
            processToKill.WaitForExit();
        }
    }
    
    [Fact]
    public void Should_Launch_Notepad_App_And_Move_Window()
    {
        // Arrange
        var windowManager = new WindowManager();
        var executablePath = @"C:\Windows\System32\notepad.exe";
        
        // Act
        var windowHandle = windowManager.LaunchAppAndGetHandle(executablePath);
        
        // Assert
        Assert.NotEqual(IntPtr.Zero, windowHandle);
        
        bool moveSuccess = windowManager.SetWindowPosition(windowHandle, 0, 0, 800, 600);
        
        Assert.True(moveSuccess);
        
        Thread.Sleep(2000);
        
        // Cleanup
        if (windowHandle != IntPtr.Zero)
        {
            int processId = windowManager.GetProcessIdFromWindowHandle(windowHandle);
            Process processToKill = Process.GetProcessById((int)processId);
            processToKill.Kill();
            processToKill.WaitForExit();
        }
    }
}
