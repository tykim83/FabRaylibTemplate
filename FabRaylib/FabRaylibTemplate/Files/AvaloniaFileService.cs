using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FabRaylibTemplate.Files
{
    public class AvaloniaFileService : IFileService
    {
        private Window GetActiveWindow()
        {
            var tempWindow = new Window { Width = 1, Height = 1, ShowInTaskbar = false };
            tempWindow.Show();
            // Optionally, you can hide it immediately.
            tempWindow.Hide();
            return tempWindow;
        }

        public async Task<Texture2D> PickFileAsync()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select an Image",
                Filters = new List<FileDialogFilter>
                    {
                        new FileDialogFilter { Name = "Image Files", Extensions = new List<string> { "png", "jpg", "jpeg", "bmp" } }
                    }
            };

            // Create a temporary hidden window to serve as the dialog's owner.
            var window = new Window { Width = 0, Height = 0, ShowInTaskbar = false };
            window.Show();

            var result = await dialog.ShowAsync(window);
            window.Close();

            if (result != null && result.Length > 0)
            {
                string filePath = result[0];
                Console.WriteLine("Picked file: " + filePath);

                // Load the image using Raylib.
                Raylib_cs.Image img = Raylib.LoadImage(filePath);

                Texture2D texture = Raylib.LoadTextureFromImage(img);
                Raylib.UnloadImage(img);
                return texture;
            }
            else
            {
                Console.WriteLine("No file selected.");
                return default;
            }
        }

        public void DownloadFile(string fileName)
        {
            // Configure a SaveFileDialog.
            var saveDialog = new SaveFileDialog
            {
                Title = "Save File",
                InitialFileName = fileName,
                Filters = new List<FileDialogFilter>
                    {
                        new FileDialogFilter { Name = "All Files", Extensions = new List<string> { "*" } }
                    }
            };

            // Create a temporary hidden window for the dialog.
            var window = new Window { Width = 0, Height = 0, ShowInTaskbar = false };
            window.Show();

            // Show the save dialog asynchronously.
            saveDialog.ShowAsync(window).ContinueWith(t =>
            {
                window.Close();
                string result = t.Result;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    try
                    {
                        File.Copy(fileName, result, overwrite: true);
                        Console.WriteLine("File saved successfully to: " + result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error saving file: " + ex.Message);
                    }
                }
            });
        }

    }

    public partial class App : Avalonia.Application
    {
        public override void Initialize()
        {
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = null;
            }
            base.OnFrameworkInitializationCompleted();
        }
    }

    public static class AvaloniaHelper
    {
        private static bool _initialized = false;
        public static void Initialize()
        {
            if (!_initialized)
            {
                AppBuilder.Configure<App>()
                          .UsePlatformDetect()
                          .SetupWithoutStarting();
                _initialized = true;
            }
        }
    }
}