using E_learningPlatform.Application.Features.LessonContents.Queries.GetAllQuery;
using E_learningPlatform.Application.Features.Lessons.DTO;
using E_learningPlatform.Domain.Enums;
using E_learningPlatform.Domain.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.LessonContents.DTO
{
    public class LessonContentVm
    {
        private readonly string _baseUrl;

        public LessonContentVm()
        {
            
        }
        public LessonContentVm(IOptions<ApiSettings> options, LessonContentIncludes includes)
        {
            _baseUrl = options.Value.BaseUrl.TrimEnd('/');

        }
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string ContentType { get; set; }
        public string? TextContent { get; set; }
        public int? DurationSeconds { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDownloadable { get; set; }
        public string? FileName { get; set; }
        public long? FileSizeBytes { get; set; }
        public string? MimeType { get; set; }
        public string? ContentUrl { get; set; }
        public string? FullContentUrl => string.IsNullOrEmpty(ContentUrl) ? null : $"{_baseUrl}/{ContentUrl.TrimStart('/').Replace("\\", "/")}";

        //public LessonVm? Lesson { get; set; }
    }
}
