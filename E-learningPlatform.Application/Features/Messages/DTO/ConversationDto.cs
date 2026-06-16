using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Features.Messages.DTO
{
    public class ConversationDto
    {
        public string OtherUserId { get; set; } = null!;
        public string OtherUserName { get; set; } = null!;
        public string? OtherUserAvatar { get; set; }
        public List<MessageDto> Messages { get; set; } = new();
        public int TotalCount { get; set; }
        public bool HasMore { get; set; }
    }
}
