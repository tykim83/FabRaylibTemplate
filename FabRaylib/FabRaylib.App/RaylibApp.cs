using Raylib_cs;

namespace FabRaylib.App;

public class RaylibApp
{
    private static IFileService? _fileService;

    private static Texture2D _logo;
    private static Texture2D _loadedImage;
    private static bool _imageLoaded = false;

    public static void Init(IFileService fileService)
    {
        _fileService = fileService;

        Raylib.InitWindow(800, 800, "FabRaylib App");
        Raylib.SetTargetFPS(60);
        var path = Path.Combine(AppContext.BaseDirectory, "Resources/raylib_logo.png");
        _logo = Raylib.LoadTexture(path);
    }

    public static void UpdateFrame()
    {
        if (Raylib.IsKeyReleased(KeyboardKey.O))
        {
            Console.WriteLine("Pressed o");
            _ = PickAndLoadTextureAsync();
        }

        if (Raylib.IsKeyReleased(KeyboardKey.D))
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Resources/raylib_logo.png");
            _fileService?.DownloadFile(path);
        }

        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.White);

        Raylib.DrawFPS(4, 4);
        Raylib.DrawText("Press O to upload an image - Press D to download the logo.", 4, 32, 20, Color.Maroon);

        Raylib.DrawTexture(_logo, 4, 64, Color.White);

        if (_imageLoaded)
        {
            Raylib.DrawTexture(_loadedImage, 200, 300, Color.White);
        }

        Raylib.EndDrawing();
    }

    private static async Task PickAndLoadTextureAsync()
    {
        var filePath = await _fileService!.PickFileAsync();
        Image img = Raylib.LoadImage(filePath);

        _loadedImage = Raylib.LoadTextureFromImage(img);
        Raylib.UnloadImage(img);
        _imageLoaded = true;
    }
}
