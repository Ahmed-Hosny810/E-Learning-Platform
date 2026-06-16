using E_learningPlatform.Application.Features.Messages.DTO;
using E_learningPlatform.Application.Features.Notifications.DTO;
using E_learningPlatform.Application.Features.QuizAttempts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Interfaces.Services
{
    public interface IRealtimeService
    {
        // Chat
        Task SendMessageAsync(string targetUserId, MessageDto message);
        Task SendMessageReadAsync(string targetUserId, int messageId);

        // Notifications
        Task SendNotificationAsync(string userId, NotificationDto notification);

        // Quiz
        Task SendTimeWarningAsync(string userId, int remainingSeconds);
        Task SendQuizExpiredAsync(string userId, int attemptId);
        Task SendQuizResultAsync(string userId, QuizResultDto result);

        // Discussion
        //Task SendNewCommentAsync(int courseId, DiscussionCommentDto comment);
    }
}
