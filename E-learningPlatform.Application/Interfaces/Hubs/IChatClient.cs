using E_learningPlatform.Application.Features.Messages.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Interfaces.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(MessageDto message);
        Task MessageRead(int messageId);
    }
}
