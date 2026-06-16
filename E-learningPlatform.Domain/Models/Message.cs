using E_learningPlatform.Domain.Common;
using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Message : BaseEntity
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int? CourseId { get; set; }  
        public string MessageText { get; set; }
        public MessageType MessageType { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? ReadAt { get; set; }

        public UserProfile Sender { get; set; } = null!;
        public UserProfile Receiver { get; set; } = null!;
        public Course? Course { get; set; }  
    }
}
