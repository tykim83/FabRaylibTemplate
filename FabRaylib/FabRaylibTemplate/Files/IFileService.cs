using Raylib_cs;
using System.Threading.Tasks;

namespace FabRaylibTemplate.Files;

public interface IFileService
{
    Task<Texture2D> PickFileAsync();
    void DownloadFile(string fileName);
}
