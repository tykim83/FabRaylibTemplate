namespace FabRaylib.App;

public interface IFileService
{
    Task<string> PickFileAsync();
    Task DownloadFileAsync(string fileName);
}
