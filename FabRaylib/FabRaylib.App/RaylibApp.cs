using Raylib_cs;

namespace FabRaylib.App;

public class RaylibApp
{
    private static IFileService? _fileService;

    private static Texture2D _logo;
    private static Texture2D _loadedImage;
    private static bool _imageLoaded = false;
    private static string _logoPath = Path.Combine(AppContext.BaseDirectory, "Resources/raylib_logo.png");

    public static void Init(IFileService fileService)
    {
        _fileService = fileService;

        Raylib.InitWindow(800, 800, "FabRaylib App");
        Raylib.SetTargetFPS(60);
        _logo = Raylib.LoadTexture(_logoPath);
    }

    public static void UpdateFrame()
    {
        if (Raylib.IsKeyReleased(KeyboardKey.O))
            _ = PickAndLoadTextureAsync();

        if (Raylib.IsKeyReleased(KeyboardKey.D))
            _fileService?.DownloadFile(_logoPath);

        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.White);

        Raylib.DrawFPS(4, 4);
        Raylib.DrawText("Press O to upload an image - Press D to download the logo.", 4, 32, 20, Color.Maroon);

        Raylib.DrawTexture(_logo, 4, 64, Color.White);

        if (_imageLoaded)
            Raylib.DrawTexture(_loadedImage, 200, 300, Color.White);

        Raylib.EndDrawing();
    }

    private static async Task PickAndLoadTextureAsync()
    {
        var filePath = await _fileService!.PickFileAsync();
        Image img = Raylib.LoadImage(filePath);

        _loadedImage = Raylib.LoadTextureFromImage(img);
        _imageLoaded = true;

        Raylib.UnloadImage(img);
    }
}
