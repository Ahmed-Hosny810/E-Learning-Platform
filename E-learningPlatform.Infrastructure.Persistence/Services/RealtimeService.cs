using E_learningPlatform.Application.Features.Messages.DTO;
using E_learningPlatform.Application.Features.Notifications.DTO;
using E_learningPlatform.Application.Features.QuizAttempts.DTO;
using E_learningPlatform.Application.Interfaces.Hubs;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Infrastructure.Persistence.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Infrastructure.Persistence.Services
{
    public class RealtimeService : IRealtimeService
    {
        private readonly IHubContext<AppHub, IAppClient> _hubContext;

        public RealtimeService(IHubContext<AppHub,IAppClient> hubContext)
        {
            _hubContext = hubContext;
        }


        #region Chat
        public async Task SendMessageAsync(string targetUserId, MessageDto message)
        {
            await _hubContext.Clients.User(targetUserId).ReceiveMessage(message);
        }

        public async Task SendMessageReadAsync(string targetUserId, int messageId)
        {
            await _hubContext.Clients.User(targetUserId).MessageRead(messageId);
        } 
        #endregion

        public async Task SendNotificationAsync(string userId, NotificationDto notification)
        {
           await _hubContext.Clients.User(userId).ReceiveNotification(notification);
        }

        public Task SendQuizExpiredAsync(string userId, int attemptId)
        {
            throw new NotImplementedException();
        }

        public Task SendQuizResultAsync(string userId, QuizResultDto result)
        {
            throw new NotImplementedException();
        }

        public Task SendTimeWarningAsync(string userId, int remainingSeconds)
        {
            throw new NotImplementedException();
        }
    }
}
