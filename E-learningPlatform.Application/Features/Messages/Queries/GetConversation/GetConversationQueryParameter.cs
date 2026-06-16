using E_learningPlatform.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Features.Messages.Queries.GetConversation
{
    public class GetConversationQueryParameter:RequestParameter<MessageOrderKey>
    {
        public MessageFilter? Filter { get; set; }
    }
    public class MessageFilter
    {
         public bool? IsRead { get; set; }
    }
    public enum MessageOrderKey
    {
        Id,
        SentAt
    }
}

