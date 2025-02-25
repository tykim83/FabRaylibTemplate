using Raylib_cs;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace FabRaylibTemplate.Files;

public partial class BrowserFileService : IFileService
{
    [JSImport("PickFile", "interop.js")]
    public static partial Task<JSObject> PickFileInteropAsync();

    [JSImport("DownloadFile", "interop.js")]
    public static partial void DownloadFileInterop(string fileName, string dataBase64);

    public void DownloadFile(string fileName)
    {
        byte[] logoBytes = System.IO.File.ReadAllBytes("Resources/raylib_logo.png");
        string base64Data = Convert.ToBase64String(logoBytes);
        DownloadFileInterop("raylib_logo.png", base64Data);
    }

    public async Task<Texture2D> PickFileAsync()
    {
        var file = await PickFileInteropAsync();
        byte[] imageData = file?.GetPropertyAsByteArray("content") ?? Array.Empty<byte>();

        string fileName = file?.GetPropertyAsString("name") ?? "temp.png";
        string filePath = "/tmp/" + fileName;
        System.IO.File.WriteAllBytes(filePath, imageData);

        Image img = Raylib.LoadImage(filePath);
        Texture2D loadedImage = Raylib.LoadTextureFromImage(img);
        Raylib.UnloadImage(img);

        return loadedImage;
    }
}
