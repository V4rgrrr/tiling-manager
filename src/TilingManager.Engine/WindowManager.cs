using System.Diagnostics;

namespace TilingManager.Engine;

public class WindowManager
{
    public IntPtr LaunchAppAndGetHandle(string executablePath)
    {
        Process process = new Process();
        process.StartInfo.FileName = executablePath;
        
        process.Start();

        while (process.MainWindowHandle == IntPtr.Zero)
        {
            process.Refresh();
            Thread.Sleep(50);
        }
        
        return process.MainWindowHandle;
    }
}