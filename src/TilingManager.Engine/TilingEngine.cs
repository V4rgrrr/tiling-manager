using TilingManager.Core.Models;

namespace TilingManager.Engine;

public class TilingEngine
{
    private readonly WindowManager _windowManager;
    private readonly ZoneCalculator _zoneCalculator;

    public TilingEngine(WindowManager windowManager, ZoneCalculator zoneCalculator)
    {
        _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        _zoneCalculator = zoneCalculator ?? throw new ArgumentNullException(nameof(zoneCalculator));
    }

    public bool TileWindow(IntPtr windowHandle, Zone zone)
    {
        if (windowHandle == IntPtr.Zero || zone == null)
            return false;
        
        var (x, y, width,height) = _zoneCalculator.CalculateAbsoluteCoordinates(zone);
        
        return _windowManager.SetWindowPosition(windowHandle, x, y, width, height);
    }
}