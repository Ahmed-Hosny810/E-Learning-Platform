using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Notification: BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string Type { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? LinkUrl { get; set; }
        public string? Metadata { get; set; }  
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }    
        public UserProfile UserProfile { get; set; } = null!;
    }
}
