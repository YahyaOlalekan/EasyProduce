using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions
{
    public interface IFileUploadServiceForWWWRoot
    {
         Task<(bool status, string name)> UploadFileAsync(IFormFile file);
    }
}