using FabRaylibTemplate.Files;
using Raylib_cs;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace FabRaylibTemplate;

public partial class Application
{
    private static readonly IFileService fileService = new BrowserFileService();

    private static Texture2D logo;
    private static Texture2D loadedImage;
    private static bool imageLoaded = false;
    public static async Task Main()
    {
        await JSHost.ImportAsync("interop.js", "../interop.js");

        Raylib.InitWindow(800, 800, "FabRaylibTemplate");
        Raylib.SetTargetFPS(60);

        logo = Raylib.LoadTexture("Resources/raylib_logo.png");
    }

    [JSExport]
    public static void UpdateFrame()
    {
        if (Raylib.IsKeyReleased(KeyboardKey.O))
        {
            Console.WriteLine("Pressed o");
            _ = PickAndLoadTextureAsync();
        }

        if (Raylib.IsKeyReleased(KeyboardKey.D))
        {
            fileService.DownloadFile("Resources/raylib_logo.png");
        }

        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.White);

        Raylib.DrawFPS(4, 4);
        Raylib.DrawText("Press O to upload an image - Press D to download the logo.", 4, 32, 20, Color.Maroon);

        Raylib.DrawTexture(logo, 4, 64, Color.White);

        if (imageLoaded)
        {
            Raylib.DrawTexture(loadedImage, 200, 300, Color.White);
        }

        Raylib.EndDrawing();
    }

    private static async Task PickAndLoadTextureAsync()
    {
        loadedImage = await fileService.PickFileAsync();
        imageLoaded = true;
    }
}
