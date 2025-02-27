using FabRaylib.App;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace FabRaylib.Template;

public partial class BrowserFileService : IFileService
{
    [JSImport("PickFile", "interop.js")]
    public static partial Task<JSObject> PickFileInteropAsync();

    [JSImport("DownloadFile", "interop.js")]
    public static partial void DownloadFileInterop(string fileName, string dataBase64);

    public void DownloadFile(string fileName)
    {
        byte[] logoBytes = System.IO.File.ReadAllBytes(fileName);
        string base64Data = Convert.ToBase64String(logoBytes);
        DownloadFileInterop("raylib_logo.png", base64Data);
    }

    public async Task<string> PickFileAsync()
    {
        var file = await PickFileInteropAsync();
        byte[] imageData = file?.GetPropertyAsByteArray("content") ?? [];

        string fileName = file?.GetPropertyAsString("name") ?? "temp.png";
        string filePath = "/tmp/" + fileName;
        System.IO.File.WriteAllBytes(filePath, imageData);

        return filePath;
    }
}
