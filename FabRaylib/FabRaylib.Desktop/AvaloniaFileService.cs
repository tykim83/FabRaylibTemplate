using Avalonia.Controls;
using Avalonia.Platform.Storage;
using FabRaylib.App;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FabRaylib.Desktop;

public class AvaloniaFileService : IFileService
{
    public async Task<string> PickFileAsync()
    {
        var window = new Window { Width = 1, Height = 1, ShowInTaskbar = false, SystemDecorations = SystemDecorations.None, Opacity = 0 };
        window.Show();

        var options = new FilePickerOpenOptions
        {
            Title = "Select an Image",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("Image Files")
                {
                    Patterns = ["*.png", "*.jpg", "*.jpeg", "*.bmp"],
                    MimeTypes = ["image/png", "image/jpeg", "image/bmp"]
                }
            ]
        };

        var result = await window.StorageProvider.OpenFilePickerAsync(options);
        window.Close();

        if (result == null || result.Count <= 0)
            return string.Empty;

        return result[0].Path.LocalPath;
    }

    public async Task DownloadFileAsync(string fileName)
    {
        string fileNameOnly = Path.GetFileName(fileName);

        var window = new Window { Width = 1, Height = 1, ShowInTaskbar = false, SystemDecorations = SystemDecorations.None, Opacity = 0 };
        window.Show();

        var options = new FilePickerSaveOptions
        {
            Title = "Save File",
            DefaultExtension = ".png",
            SuggestedFileName = fileNameOnly,
            FileTypeChoices = new List<FilePickerFileType>
            {
                {
                    new FilePickerFileType("All Files") { Patterns = ["*.*"] }
                }
            }
        };

        IStorageFile? result = await window.StorageProvider.SaveFilePickerAsync(options);
        await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() => window.Close());

        if (result == null)
            return;

        string destinationPath = result.Path.LocalPath;
        File.Copy(fileName, destinationPath, overwrite: true);
    }
}
