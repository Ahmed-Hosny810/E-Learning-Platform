using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Settings
{
    public class FileUploadSettings
    {
        public int MaxFileSizeMB { get; set; }
        public List<string> AllowedFileExtensions { get; set; } = new();
        public List<string> AllowedFileMimeTypes { get; set; } = new();
        public string VideoStoragePath { get; set; } = string.Empty;
        public string DocumentsStoragePath { get; set; } = string.Empty;
        public string ThumbnailStoragePath { get; set; } = string.Empty;
        public string DefaultStoragePath { get; set; } = string.Empty;
    }
}
