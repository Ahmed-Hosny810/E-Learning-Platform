using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Services
{
    //public class FileService : IFileService
    //{
    //    private readonly string _baseUrl;

    //    public FileService(IOptions<ApiSettings> options)
    //    {
    //        _baseUrl = options.Value.BaseUrl.TrimEnd('/');
    //    }

    //    public string GetFullUrl(string relativePath)
    //    {
    //        if (string.IsNullOrEmpty(relativePath)) return null;
    //        if (relativePath.StartsWith("http")) return relativePath;

    //        return $"{_baseUrl}/{relativePath.TrimStart('/').Replace("\\", "/")}";
    //    }
    //}
}