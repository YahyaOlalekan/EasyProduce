
using System;
using System.IO;
using System.Threading.Tasks;
using Application.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;


namespace Persistence
{
    public class FileUploadServiceForWWWRoot : IFileUploadServiceForWWWRoot
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IValidateImage _validateImage;

        public FileUploadServiceForWWWRoot(IWebHostEnvironment webHostEnvironment, IValidateImage validateImage)
        {
            _webHostEnvironment = webHostEnvironment;
            _validateImage = validateImage;
        }

        public async Task<(bool status, string name)> UploadFileAsync(IFormFile file)
        {
            var validationResponse = _validateImage.Validate(file);
            if (!validationResponse.status)
            {
                return (false, validationResponse.message); 
            }

            var appUploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload/images");
            if (!Directory.Exists(appUploadPath))
            {
                Directory.CreateDirectory(appUploadPath);
            }
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var fullPath = Path.Combine(appUploadPath, fileName);
            file.CopyTo(new FileStream(fullPath, FileMode.Create));
            return (true, fileName); 
        }



    }
}