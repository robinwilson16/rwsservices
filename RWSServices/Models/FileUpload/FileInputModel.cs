using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RWSServices.Models.FileUpload
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
        public string Subfolder { get; set; }
    }
}
