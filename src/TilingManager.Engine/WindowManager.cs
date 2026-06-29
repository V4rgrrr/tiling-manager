using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TilingManager.Engine;

public class WindowManager
{
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

    public bool SetWindowPosition(IntPtr windowHandle, int x, int y, int width, int height)
    {
        if (windowHandle == IntPtr.Zero)
            return false;
        
        return NativeMethods.MoveWindow(windowHandle, x, y, width, height, true);
    }
    
    public int GetProcessIdFromWindowHandle(IntPtr windowHandle)
    {
        NativeMethods.GetWindowThreadProcessId(windowHandle, out uint processId);
        return (int)processId;
    }


    private static HashSet<IntPtr> GetVisibleWindowsForProcessName(string processName)
    {
        HashSet<IntPtr> windows = new HashSet<IntPtr>();
        
        Process[] processes = Process.GetProcessesByName(processName);
        HashSet<uint> pids = new HashSet<uint>();

        foreach (var p in processes)
        {
            pids.Add((uint)p.Id);
            p.Dispose();
        }

        NativeMethods.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
        {
            if (NativeMethods.IsWindowVisible(hWnd))
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint windowPid);
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