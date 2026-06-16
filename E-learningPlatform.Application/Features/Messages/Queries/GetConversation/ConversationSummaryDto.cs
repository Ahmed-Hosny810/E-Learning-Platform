using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Features.Messages.Queries.GetConversation
{
    public class ConversationSummaryDto
    {
        public string ContactId { get; set; } = null!;
        public string ContactName { get; set; } = null!;
        public string LastMessageText { get; set; } = null!;
        public DateTime LastMessageAt { get; set; }
        public int UnreadCount { get; set; }
    }
}
