using Raylib_cs;
using System.Runtime.InteropServices.JavaScript;

namespace FabRaylibTemplate;

public partial class Application
{
    private static Texture2D logo;

    public static void Main()
    {
        Raylib.InitWindow(512, 512, "FabRaylibTemplate");
        Raylib.SetTargetFPS(60);

        logo = Raylib.LoadTexture("Resources/raylib_logo.png");
    }

    [JSExport]
    public static void UpdateFrame()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.White);

        Raylib.DrawFPS(4, 4);
        Raylib.DrawText("All systems operational!", 4, 32, 20, Color.Maroon);

        Raylib.DrawTexture(logo, 4, 64, Color.White);

        Raylib.EndDrawing();
    }
}
