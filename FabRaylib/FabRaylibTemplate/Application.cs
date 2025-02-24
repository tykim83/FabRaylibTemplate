using Raylib_cs;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace FabRaylibTemplate;

public partial class Application
{
    [JSImport("PickFile", "interop.js")]
    public static partial Task<JSObject> PickFileAsync();

    [JSImport("DownloadFile", "interop.js")]
    public static partial void DownloadFile(string fileName, string dataBase64);


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

            _ = PickAndLoadImageAsync();
        }

        if (Raylib.IsKeyReleased(KeyboardKey.D))
        {
            Console.WriteLine("Pressed d");
            try
            {
                byte[] logoBytes = System.IO.File.ReadAllBytes("Resources/raylib_logo.png");
                string base64Data = Convert.ToBase64String(logoBytes);
                DownloadFile("raylib_logo.png", base64Data);
                Console.WriteLine("Download triggered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Download error: " + ex.Message);
            }
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

    private static async Task PickAndLoadImageAsync()
    {
        try
        {
            Console.WriteLine("Run async image load");
            var file = await PickFileAsync();
            byte[] imageData = file?.GetPropertyAsByteArray("content") ?? Array.Empty<byte>();

            string tempPath = "/temp.png";
            System.IO.File.WriteAllBytes(tempPath, imageData);

            if (imageLoaded)
                Raylib.UnloadTexture(loadedImage);

            Image img = Raylib.LoadImage(tempPath);
            loadedImage = Raylib.LoadTextureFromImage(img);
            Raylib.UnloadImage(img);

            imageLoaded = true;
            Console.WriteLine("Image loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("PickAndLoadImageAsync Exception: " + ex.Message);
        }
    }
}
