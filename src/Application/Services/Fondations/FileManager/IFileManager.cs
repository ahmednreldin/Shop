using Microsoft.AspNetCore.Http;

namespace Application.Services.Fondations.FileManager
{
    public interface IFileManager
    {
        string SaveImage(IFormFile file);
    }
}
