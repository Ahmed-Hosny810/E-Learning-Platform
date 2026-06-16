using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Features.Messages.DTO
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public string MessageText { get; set; } = null!;
        public string MessageType { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
    }
}
