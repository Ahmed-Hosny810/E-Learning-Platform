using AutoMapper;
using E_learningPlatform.Application.Features.Notifications.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Domain.Constants;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Infrastructure.Persistence.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepositoryAsync _notificationRepo;
        private readonly IRealtimeService _realtimeService;
        private readonly IMapper _mapper;

        public NotificationService(
            INotificationRepositoryAsync notificationRepo,
            IRealtimeService realtimeService,
            IMapper mapper)
        {
            _notificationRepo = notificationRepo;
            _realtimeService = realtimeService;
            _mapper = mapper;
        }

        public async Task NotifyNewLessonAddedAsync(string userId, string courseTitle, string lessonTitle, int lessonId)
        {
            await CreateAndPushAsync(new Notification
            {
                UserId = userId,
                Type = NotificationType.LessonAdded,
                Title = "New Lesson Added",
                Message = $"New lesson '{lessonTitle}' added to {courseTitle}",
                LinkUrl = $"/Lessons/{lessonId}",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task NotifyQuizSubmittedAsync(
            string studentId, int attemptId, string quizTitle,
            int score, int totalPoints, bool isPassed)
        {
            var passed = isPassed ? "Passed ✓" : "Failed ✗";

            await CreateAndPushAsync(new Notification
            {
                UserId = studentId,
                Type = NotificationType.QuizSubmitted,
                Title = $"Quiz Result — {passed}",
                Message = $"{quizTitle}: {score}/{totalPoints}",
                LinkUrl = $"/attempts/{attemptId}/result",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task NotifyQuizExpiredAsync(
            string studentId, int attemptId, string quizTitle)
        {
            await CreateAndPushAsync(new Notification
            {
                UserId = studentId,
                Type = NotificationType.QuizExpired,
                Title = "Quiz Time Expired",
                Message = $"Time ran out for: {quizTitle}",
                LinkUrl = $"/attempts/{attemptId}/result",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task NotifyCoursePublishedAsync(
            string userId, int courseId, string courseTitle)
        {
            await CreateAndPushAsync(new Notification
            {
                UserId = userId,
                Type = NotificationType.CoursePublished,
                Title = "New Course Available",
                Message = $"{courseTitle} is now available",
                LinkUrl = $"/courses/{courseId}",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        // ── Helper ───────────────────────────────────────────────────────

        private async Task CreateAndPushAsync(Notification notification)
        {
            await _notificationRepo.AddAsync(notification);

            await _realtimeService.SendNotificationAsync(
                notification.UserId,
                _mapper.Map<NotificationDto>(notification));
        }
    }
}
