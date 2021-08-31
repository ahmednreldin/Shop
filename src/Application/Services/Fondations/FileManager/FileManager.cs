using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Shop.Web.Services.Fondations.FileManager
{
    public class FileManager : IFileManager
    {
        IConfiguration Configuration;

        public FileManager(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public string SaveImage(IFormFile image)
        {
            if (image == null) return "";

            string imagePath = Configuration["Path:Images"];

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HHH-mm-ss")}{mime}";

            using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return fileName;
        }


    }
}
