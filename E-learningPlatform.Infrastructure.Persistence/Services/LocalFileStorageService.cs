using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _contentRootPath;
        public LocalFileStorageService(IOptions<FileUploadSettings> settings,
            IWebHostEnvironment environment)
        {
            _contentRootPath = environment.ContentRootPath;
        }
        public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string baseFolderPath, CancellationToken ct = default)
        {
            try
            {
                var uniqueFileName= $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
                var dateFolder = Path.Combine(
                    DateTime.UtcNow.Year.ToString(),
                    DateTime.UtcNow.Month.ToString("00")
                );
                // Combine: App_Data/uploads/videos/2026/02
                var relativeFolderPath = Path.Combine(baseFolderPath, dateFolder);
                // Combine: App_Data/uploads/videos/2026/02
                var absoluteFolderPath = Path.Combine(_contentRootPath, relativeFolderPath);

                if (!Directory.Exists(absoluteFolderPath))
                    Directory.CreateDirectory(absoluteFolderPath);

                //Full path: uploads/videos/2026/02/uniqueFileName.ext
                var uploadPath = Path.Combine(absoluteFolderPath, uniqueFileName);

                if (fileStream.CanSeek)
                    fileStream.Position = 0;
               
                using (var fileStreamOutput = new FileStream(
                   uploadPath,
                   FileMode.Create,
                   FileAccess.Write,
                   FileShare.None,
                   bufferSize: 4096,
                   useAsync: true))
               {
                   await fileStream.CopyToAsync(fileStreamOutput, ct);
                   await fileStreamOutput.FlushAsync(ct);
               }

                // 5. Return URL-friendly path
                //   App_Data/uploads/videos/2026/02/uniqueFileName
                return Path.Combine(relativeFolderPath, uniqueFileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {

                throw new IOException($"Failed to save video file: {ex.Message}", ex);
            }
        }
        public void DeleteFile(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return;

            try
            {
                
                string absolutePath = Path.Combine(_contentRootPath, relativePath.TrimStart('/', '\\'));

                // 3. Check if the file actually exists on the disk
                if (File.Exists(absolutePath))
                {
                    File.Delete(absolutePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }
        public bool IsValidFile(Stream file, string fileName, string mimeType,
                             string[] allowedExtensions, string[] allowedMimeTypes, long maxSizeBytes)
        {
            if (file.Length > maxSizeBytes) return false;

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension)) return false;

            if (!allowedMimeTypes.Contains(mimeType)) return false;

            return true;
        }

        //public bool IsValidFile(Stream file, string fileName, string[] allowedExtensions, long maxSizeBytes)
        //{
        //    if (file.Length > maxSizeBytes) return false;

        //    // Check Extension
        //    var extension = Path.GetExtension(fileName).ToLowerInvariant();
        //    return allowedExtensions.Contains(extension);
        //}

    }
}
