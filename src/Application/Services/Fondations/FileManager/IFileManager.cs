using Microsoft.AspNetCore.Http;

namespace Shop.Web.Services.Fondations.FileManager
{
    public interface IFileManager
    {
        string SaveImage(IFormFile file);
    }
}
