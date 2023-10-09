using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace Persistence
{
    public class ValidateImage : IValidateImage
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        // Maximum allowed image dimensions
        private const int MaxWidth = 1920;
        private const int MaxHeight = 1080;

        // public static string Validate(IFormFile file)
        public string Validate(IFormFile file)
        {
            if (file == null || file.Length == 0) //Length property => size of the uploaded file in bytes
            {
                return "No file or empty file"; 
            }

            // Check file type (extension)
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                return "Invalid file type"; 
            }

            // Check file size
            if (file.Length > MaxFileSize)
            {
                return "File size exceeds the limit"; // File size exceeds the limit
            }

            // // Check content type (MIME type)
            // if (!IsSupportedContentType(file.ContentType))
            // {
            //     return "Unsupported content type"; // Unsupported content type
            // }

            // Check image dimensions
            try
            {
                using (var image = Image.Load(file.OpenReadStream()))
                {
                    if (image.Width > MaxWidth || image.Height > MaxHeight)
                    {
                        return "Image dimensions exceed the limit"; // Image dimensions exceed the limit
                    }
                }
            }
            catch (Exception)
            {
                return "An error occurred when loading the image"; // An exception occurred when loading the image
            }

            return null; // No validation error
        }

        // private static bool IsSupportedContentType(string contentType)
        // {
        //     // Customize this list as needed based on supported content types
        //     var supportedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        //     return supportedContentTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase);
        // }
    }

}