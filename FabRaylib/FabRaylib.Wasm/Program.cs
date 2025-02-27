using FabRaylib.App;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace FabRaylib.Template;

public partial class Program
{
    private static readonly IFileService _fileService = new BrowserFileService();

    public static async Task Main()
    {
        await JSHost.ImportAsync("interop.js", "../interop.js");

        RaylibApp.Init(_fileService);
    }

    [JSExport]
    public static void UpdateFrame()
    {
        RaylibApp.UpdateFrame();
    }
}
