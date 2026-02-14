using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName, string rootFolder, CancellationToken ct = default);
        void DeleteFile(string relativePath);
        //bool IsValidFile(Stream file, string fileName, string[] allowedExtensions, long maxSizeBytes);
        bool IsValidFile(Stream file, string fileName, string mimeType,
                         string[] allowedExtensions, string[] allowedMimeTypes, long maxSizeBytes);
    }
}

//namespace E_learningPlatform.Infrastructure.Services
//{
//    public class LocalFileStorageService : IFileStorageService
//    {
//        private readonly FileUploadSettings _settings;
//        private readonly string _webRootPath;

//        public LocalFileStorageService(
//            IOptions<FileUploadSettings> settings,
//            IWebHostEnvironment environment)
//        {
//            _settings = settings.Value;
//            _webRootPath = environment.WebRootPath;

//            EnsureDirectoriesExist();
//        }

//        public async Task<string> SaveVideoAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                // Generate unique filename
//                var uniqueFileName = GenerateUniqueFileName(fileName);

//                // Organize by date for better file management
//                var subFolder = Path.Combine(
//                    DateTime.UtcNow.Year.ToString(),
//                    DateTime.UtcNow.Month.ToString("00")
//                );

//                // Create full directory path
//                var uploadPath = Path.Combine(_webRootPath, _settings.VideoStoragePath, subFolder);

//                // Ensure directory exists
//                if (!Directory.Exists(uploadPath))
//                {
//                    Directory.CreateDirectory(uploadPath);
//                }

//                // Full file path
//                var filePath = Path.Combine(uploadPath, uniqueFileName);

//                // Save file
//                using (var fileStreamOutput = new FileStream(
//                    filePath,
//                    FileMode.Create,
//                    FileAccess.Write,
//                    FileShare.None,
//                    bufferSize: 4096,
//                    useAsync: true))
//                {
//                    await fileStream.CopyToAsync(fileStreamOutput, cancellationToken);
//                    await fileStreamOutput.FlushAsync(cancellationToken);
//                }

//                // Return relative path for database storage
//                var relativePath = Path.Combine(_settings.VideoStoragePath, subFolder, uniqueFileName);
//                return relativePath.Replace("\\", "/");
//            }
//            catch (Exception ex)
//            {
//                throw new IOException($"Failed to save video file: {ex.Message}", ex);
//            }
//        }

//        public async Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                var fullPath = Path.Combine(_webRootPath, filePath);

//                if (File.Exists(fullPath))
//                {
//                    await Task.Run(() => File.Delete(fullPath), cancellationToken);
//                    return true;
//                }

//                return false;
//            }
//            catch (Exception ex)
//            {
//                throw new IOException($"Failed to delete file: {ex.Message}", ex);
//            }
//        }

//        public async Task<Stream> GetFileStreamAsync(string filePath, CancellationToken cancellationToken = default)
//        {
//            var fullPath = Path.Combine(_webRootPath, filePath);

//            if (!File.Exists(fullPath))
//            {
//                throw new FileNotFoundException($"File not found: {filePath}", fullPath);
//            }

//            return await Task.FromResult(
//                new FileStream(
//                    fullPath,
//                    FileMode.Open,
//                    FileAccess.Read,
//                    FileShare.Read,
//                    bufferSize: 4096,
//                    useAsync: true)
//            );
//        }

//        public async Task<bool> FileExistsAsync(string filePath, CancellationToken cancellationToken = default)
//        {
//            var fullPath = Path.Combine(_webRootPath, filePath);
//            return await Task.FromResult(File.Exists(fullPath));
//        }

//        public async Task<long> GetFileSizeAsync(string filePath, CancellationToken cancellationToken = default)
//        {
//            var fullPath = Path.Combine(_webRootPath, filePath);

//            if (!File.Exists(fullPath))
//            {
//                throw new FileNotFoundException($"File not found: {filePath}", fullPath);
//            }

//            var fileInfo = new FileInfo(fullPath);
//            return await Task.FromResult(fileInfo.Length);
//        }

//        public string GetFileUrl(string filePath)
//        {
//            return $"/{filePath.Replace("\\", "/")}";
//        }

//        private void EnsureDirectoriesExist()
//        {
//            var videoPath = Path.Combine(_webRootPath, _settings.VideoStoragePath);
//            var thumbnailPath = Path.Combine(_webRootPath, _settings.ThumbnailStoragePath);
//            var tempPath = Path.Combine(_webRootPath, _settings.TempStoragePath);

//            Directory.CreateDirectory(videoPath);
//            Directory.CreateDirectory(thumbnailPath);
//            Directory.CreateDirectory(tempPath);
//        }

//        private string GenerateUniqueFileName(string originalFileName)
//        {
//            var extension = Path.GetExtension(originalFileName);
//            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);

//            // Sanitize filename
//            fileNameWithoutExtension = string.Join("_",
//                fileNameWithoutExtension.Split(Path.GetInvalidFileNameChars()));

//            // Limit length
//            if (fileNameWithoutExtension.Length > 50)
//            {
//                fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, 50);
//            }

//            // Create unique filename
//            return $"{Guid.NewGuid()}_{fileNameWithoutExtension}{extension}";
//        }
//    }
