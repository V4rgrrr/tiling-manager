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
        
        // Cleanup - not working ...
        if (windowHandle != IntPtr.Zero)
        {
            Process processToKill = Process.GetProcessById((int)windowHandle);
            
            processToKill.Kill();
            
            processToKill.WaitForExit();
        }
    }
}
