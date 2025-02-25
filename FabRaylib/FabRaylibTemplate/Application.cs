using FabRaylibTemplate.Files;
using Raylib_cs;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace FabRaylibTemplate;

public partial class Application
{
    private static readonly IFileService fileService = CreateFileService();

    private static IFileService CreateFileService()
    {
        return OperatingSystem.IsBrowser()
            ? new BrowserFileService()
            : new AvaloniaFileService();
    }

    private static Texture2D logo;
    private static Texture2D loadedImage;
    private static bool imageLoaded = false;
    public static async Task Main()
    {
        if (OperatingSystem.IsBrowser())
            await JSHost.ImportAsync("interop.js", "../interop.js");
        else
            FabRaylibTemplate.Files.AvaloniaHelper.Initialize();

        Raylib.InitWindow(800, 800, "FabRaylibTemplate");
        Raylib.SetTargetFPS(60);

        logo = Raylib.LoadTexture("Resources/raylib_logo.png");

        if (!OperatingSystem.IsBrowser())
        {
            while (!Raylib.WindowShouldClose())
            {
                UpdateFrame();
            }
            Raylib.CloseWindow();
        }
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
