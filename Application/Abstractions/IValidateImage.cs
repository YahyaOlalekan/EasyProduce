using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions
{
    public interface IValidateImage
    {
       (bool status, string message) Validate(IFormFile file);

    }
}