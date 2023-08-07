
using System;
using System.IO;
using System.Threading.Tasks;
using Application.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Persistence
{
    public class FileUploadServiceForWWWRoot : IFileUploadServiceForWWWRoot
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
       
        public FileUploadServiceForWWWRoot(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
       
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var appUploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload/images");
            if (!Directory.Exists(appUploadPath))
            {
                Directory.CreateDirectory(appUploadPath);
            }
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var fullPath = Path.Combine(appUploadPath, fileName);
            file.CopyTo(new FileStream(fullPath, FileMode.Create));
            return fileName;
         }


        // public Task<string> FileUploadingAsync(IFormFile model)
        // {
        //     var imageName = "";
        //     if (model.ImageUrl != null)
        //     {
        //         var imgPath = _webHostEnvironment.WebRootPath;
        //         var imagePath = Path.Combine(imgPath, "Images");
        //         Directory.CreateDirectory(imagePath);
        //         var imageType = model.ImageUrl.ContentType.Split('/')[1];
        //         imageName = $"{Guid.NewGuid()}.{imageType}";
        //         var fullPath = Path.Combine(imagePath, imageName);
        //         using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //         {
        //             model.ImageUrl.CopyTo(fileStream);
        //         }
                
        //     }
        // }



    }
}