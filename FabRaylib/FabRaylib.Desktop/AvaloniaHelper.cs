using Avalonia;

namespace FabRaylib.Desktop;

public static class AvaloniaHelper
{
    private static bool _initialized = false;
    public static void Initialize()
    {
        if (!_initialized)
        {
            AppBuilder.Configure<AvaloniaApp>()
                      .UsePlatformDetect()
                      .SetupWithoutStarting();
            _initialized = true;
        }
    }
}
