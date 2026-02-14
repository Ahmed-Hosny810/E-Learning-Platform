using E_learningPlatform.Domain.Common;
using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class LessonContent: BaseEntity
    {
        public int LessonId { get; set; }
        public LessonContentType ContentType { get; set; }
        public string? ContentUrl { get; set; }
        public string? TextContent { get; set; }
        public int? DurationSeconds { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDownloadable { get; set; }

        
        public string? FileName { get; set; }
        public long? FileSizeBytes { get; set; }
        public string? MimeType { get; set; }
        public VideoProcessingStatus? ProcessingStatus { get; set; }
        public string? ThumbnailUrl { get; set; }

        public Lesson? Lesson { get; set; }
    }
}
