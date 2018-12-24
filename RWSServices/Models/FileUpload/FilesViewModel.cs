using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RWSServices.Models.FileUpload
{
    public class FileDetails
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDirectory { get; set; }

        public string Subfolder
        {
            get
            {
                string subfld = Path;
                //Remove file name from end
                if (subfld.LastIndexOf("\\") > 0)
                {
                    subfld = subfld.Substring(0, Path.LastIndexOf("\\"));
                }

                //Now remove everything left of the upload folder
                if (subfld.LastIndexOf("wwwroot\\Uploads") > 0)
                {
                    subfld = subfld.Substring(Path.LastIndexOf("wwwroot\\Uploads") + 15);
                }

                if (IsDirectory)
                {
                    if (subfld.Length > 0)
                    {
                        subfld += "\\" + Name;
                    }
                    else
                    {
                        subfld += Name;
                    }
                }

                return subfld;
            }
        }
    }

    public class FilesViewModel
    {
        public List<FileDetails> Files { get; set; }
            = new List<FileDetails>();
    }
}
