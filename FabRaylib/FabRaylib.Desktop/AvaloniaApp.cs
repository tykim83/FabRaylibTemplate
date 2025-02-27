using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace FabRaylib.Desktop;

public partial class AvaloniaApp : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = null;
        }
        base.OnFrameworkInitializationCompleted();
    }
}
