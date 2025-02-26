namespace FabRaylib.App;

public interface IFileService
{
    Task<string> PickFileAsync();
    void DownloadFile(string fileName);
}
