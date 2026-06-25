using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TilingManager.Engine;

public class WindowManager
{
    // Win32 API
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumProc, IntPtr lParam);
    
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    
    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);
    
    
    public IntPtr LaunchAppAndGetHandle(string executablePath)
    {
        string processName = Path.GetFileNameWithoutExtension(executablePath);
        
        HashSet<IntPtr> existingWindows = GetVisibleWindowsForProcessName(processName);
        
        Process.Start(executablePath);
        
        IntPtr newWindowHandle = IntPtr.Zero;

        for (int i = 0; i < 50; i++)
        {
            HashSet<IntPtr> currentWindows = GetVisibleWindowsForProcessName(processName);

            foreach (IntPtr hwnd in currentWindows)
            {
                if (!existingWindows.Contains(hwnd))
                {
                    newWindowHandle = hwnd;
                    break;
                }
            }

            if (newWindowHandle != IntPtr.Zero)
                break;
            
            Thread.Sleep(100);
        }
        
        return newWindowHandle;
    }
    
    public int GetProcessIdFromWindowHandle(IntPtr windowHandle)
    {
        GetWindowThreadProcessId(windowHandle, out uint processId);
        return (int)processId;
    }


    private static HashSet<IntPtr> GetVisibleWindowsForProcessName(string processName)
    {
        HashSet<IntPtr> windows = new HashSet<IntPtr>();
        
        Process[] processes = Process.GetProcessesByName(processName);
        HashSet<uint> pids = new HashSet<uint>();
        foreach (var p in processes)
            pids.Add((uint)p.Id);

        EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
        {
            if (IsWindowVisible(hWnd))
            {
                GetWindowThreadProcessId(hWnd, out uint windowPid);
                if (pids.Contains(windowPid))
                {
                    windows.Add(hWnd);
                }
            }

            return true;
        }, IntPtr.Zero);
        
        return windows;
    }
}