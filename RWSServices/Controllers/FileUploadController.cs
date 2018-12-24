using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using RWSServices.Models.FileUpload;

namespace RWSServices.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IFileProvider fileProvider;

        //For obtaining the root upload directory
        private IConfiguration _configuration;

        public FileUploadController(IFileProvider fileProvider, IConfiguration Configuration)
        {
            this.fileProvider = fileProvider;
            _configuration = Configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string subfolder)
        {
            string uploadPath = GetFolderPath(subfolder);

            if (file == null || file.Length == 0)
                return Content("file not selected");

            //Check if directory exists and create if not
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), uploadPath,
                        file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Files");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, string subfolder)
        {
            string uploadPath = GetFolderPath(subfolder);

            if (files == null || files.Count == 0)
                return Content("files not selected");

            //Check if directory exists and create if not
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), uploadPath,
                        file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Files");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UploadFileViaModel(FileInputModel model, string subfolder)
        {
            string uploadPath = GetFolderPath(subfolder);

            if (model == null ||
                model.FileToUpload == null || model.FileToUpload.Length == 0)
                return Content("file not selected");

            //Check if directory exists and create if not
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), uploadPath,
                        model.FileToUpload.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.FileToUpload.CopyToAsync(stream);
            }

            return RedirectToAction("Files");
        }

        [Authorize(Roles = "Admin, Manager, Member")]
        public IActionResult Files(string subfolder)
        {

            if (string.IsNullOrEmpty(subfolder))
            {
                subfolder = "";
            }

            var model = new FilesViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(subfolder))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath, IsDirectory = item.IsDirectory });
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, Manager, Member")]
        public async Task<IActionResult> Download(string filename, string subfolder)
        {
            string uploadPath = GetFolderPath(subfolder);

            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(), uploadPath,
                           filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        [Authorize(Roles = "Admin, Manager, Member")]
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        [Authorize(Roles = "Admin, Manager, Member")]
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        [Authorize(Roles = "Admin, Manager, Member")]
        private string GetFolderPath(string subfolder)
        {
            var baseFolder = _configuration["UploadDirectory"];

            if (string.IsNullOrEmpty(subfolder))
            {
                subfolder = "";
            }

            //If base folder does not end in a forward slash then add one
            if (baseFolder.Substring(baseFolder.Length - 1) != "/")
            {
                baseFolder += '/';
            }

            var uploadFolder = baseFolder + subfolder;

            return uploadFolder;
        }
    }
}